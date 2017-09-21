using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Threading;
using Abp.Timing;
using Abp.UI;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Mvc;
using DFF.Freedom.Authorization;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Web.Models.Account;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Controllers;
using DFF.Freedom.Identity;
using DFF.Freedom.Sessions;
using DFF.Freedom.Web.Views.Shared.Components.TenantChange;
using Microsoft.AspNetCore.Identity;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 账户 控制器
    /// </summary>
    public class AccountController : FreedomControllerBase
    {
        #region 私有属性

        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ITenantCache _tenantCache;
        private readonly INotificationPublisher _notificationPublisher; 

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="multiTenancyConfig"></param>
        /// <param name="tenantManager"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="abpLoginResultTypeHelper"></param>
        /// <param name="logInManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="userRegistrationManager"></param>
        /// <param name="sessionAppService"></param>
        /// <param name="tenantCache"></param>
        /// <param name="notificationPublisher"></param>
        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            UserRegistrationManager userRegistrationManager,
            ISessionAppService sessionAppService,
            ITenantCache tenantCache,
            INotificationPublisher notificationPublisher)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _userRegistrationManager = userRegistrationManager;
            _sessionAppService = sessionAppService;
            _tenantCache = tenantCache;
            _notificationPublisher = notificationPublisher;
        } 

        #endregion

        #region Login / Logout

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userNameOrEmailAddress">用户名或Email地址</param>
        /// <param name="returnUrl">返回链接</param>
        /// <param name="successMessage">成功消息</param>
        /// <returns></returns>
        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }

            return View(new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                MultiTenancySide = AbpSession.MultiTenancySide
            });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="returnUrl">返回链接</param>
        /// <param name="returnUrlHash"></param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, GetTenancyNameOrNull());

            await _signInManager.SignInAsync(loginResult.Identity, loginModel.RememberMe);
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        /// <summary>
        /// 退出、注销
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 获取登录结果-异步
        /// </summary>
        /// <param name="usernameOrEmailAddress">用户名或Email地址</param>
        /// <param name="password">密码</param>
        /// <param name="tenancyName">租户名称</param>
        /// <returns></returns>
        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        #endregion

        #region Register

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return RegisterView(new RegisterViewModel());
        }

        /// <summary>
        /// 注册视图
        /// </summary>
        /// <param name="model">注册视图模型</param>
        /// <returns></returns>
        private ActionResult RegisterView(RegisterViewModel model)
        {
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled; //多租户是否启用。

            return View("Register", model);
        }

        /// <summary>
        /// 是否启用自注册
        /// </summary>
        /// <returns></returns>
        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            { //如果租户Id不为空
                return false; //No registration enabled for host users! 无法为主机用户启用注册！
            }

            return true;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model">注册视图模型</param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    { //外部登录信息为空
                        throw new Exception("Can not external login!"); //不能外部登录
                    }

                    model.UserName = model.EmailAddress;
                    model.Password = Authorization.Users.User.CreateRandomPassword(); //创建随机密码
                }
                else
                {
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    { //如果用户名或密码为空
                        throw new UserFriendlyException(L("FormIsNotValidMessage")); //表单不是有效的消息
                    }
                }

                var user = await _userRegistrationManager.RegisterAsync(
                    model.Name,
                    model.Surname,
                    model.EmailAddress,
                    model.UserName,
                    model.Password,
                    true //Assumed email address is always confirmed. Change this if you want to implement email confirmation.
                );

                //Getting tenant-specific settings
                //获取特定租户的设置
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

                if (model.IsExternalLogin)
                {
                    Debug.Assert(externalLoginInfo != null);
                    
                    if (string.Equals(externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email), model.EmailAddress, StringComparison.OrdinalIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }

                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider = externalLoginInfo.LoginProvider,
                            ProviderKey = externalLoginInfo.ProviderKey,
                            TenantId = user.TenantId
                        }
                    };
                }

                await _unitOfWorkManager.Current.SaveChangesAsync();

                Debug.Assert(user.TenantId != null);

                var tenant = await _tenantManager.GetByIdAsync(user.TenantId.Value);

                //Directly login if possible
                //如果可能的话直接登录
                if (user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin))
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if (externalLoginInfo != null)
                    {
                        loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenant.TenancyName);
                    }
                    else
                    {
                        loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
                    }

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await _signInManager.SignInAsync(loginResult.Identity, false);
                        return Redirect(GetAppHomeUrl());
                    }

                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }

                return View("RegisterResult", new RegisterResultViewModel
                {
                    TenancyName = tenant.TenancyName,
                    NameAndSurname = user.Name + " " + user.Surname,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsActive = user.IsActive,
                    IsEmailConfirmationRequiredForLogin = isEmailConfirmationRequiredForLogin
                });
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }

        #endregion

        #region External Login

        /// <summary>
        /// 外部登录
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl">返回链接</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(
                "ExternalLoginCallback",
                "Account",
                new
                {
                    ReturnUrl = returnUrl,
                    authSchema = provider
                });

            return Challenge(
                new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
                {
                    Items = { { "LoginProvider", provider } },
                    RedirectUri = redirectUrl
                },
                provider
            );
        }

        /// <summary>
        /// 外部登录回调函数
        /// </summary>
        /// <param name="returnUrl">返回链接</param>
        /// <param name="authSchema"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string authSchema, string remoteError = null)
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            
            if (remoteError != null)
            {
                Logger.Error("Remote Error in ExternalLoginCallback: " + remoteError);
                throw new UserFriendlyException(L("CouldNotCompleteLoginOperation"));
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync(authSchema);
            if (externalLoginInfo == null)
            {
                Logger.Warn("Could not get information from external login.");
                return RedirectToAction(nameof(Login));
            }

            await _signInManager.SignOutAsync();

            var tenancyName = GetTenancyNameOrNull();

            var loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    await _signInManager.SignInAsync(loginResult.Identity, false);
                    return Redirect(returnUrl);
                case AbpLoginResultType.UnknownExternalLogin:
                    return await RegisterForExternalLogin(externalLoginInfo);
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        loginResult.Result,
                        externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? externalLoginInfo.ProviderKey,
                        tenancyName
                    );
            }
        }

        /// <summary>
        /// 外部登录注册
        /// </summary>
        /// <param name="externalLoginInfo">外部登录注册信息</param>
        /// <returns></returns>
        private async Task<ActionResult> RegisterForExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var nameinfo = ExternalLoginInfoHelper.GetNameAndSurnameFromClaims(externalLoginInfo.Principal.Claims.ToList());

            var viewModel = new RegisterViewModel
            {
                EmailAddress = email,
                Name = nameinfo.name,
                Surname = nameinfo.surname,
                IsExternalLogin = true,
                ExternalLoginAuthSchema = externalLoginInfo.LoginProvider
            };

            if (nameinfo.name != null &&
                nameinfo.surname != null &&
                email != null)
            {
                return await Register(viewModel);
            }

            return RegisterView(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login">用户登录信息</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual async Task<List<Tenant>> FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> allUsers;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                allUsers = await _userManager.FindAllAsync(login);
            }

            return allUsers
                .Where(u => u.TenantId != null)
                .Select(u => AsyncHelper.RunSync(() => _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// 重定向到应用程序主页
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 获取应用程序主页Url链接
        /// </summary>
        /// <returns></returns>
        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home");
        }

        #endregion

        #region Change Tenant

        /// <summary>
        /// 租户更改模态框
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TenantChangeModal()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            return View("/Views/Shared/Components/TenantChange/_ChangeModal.cshtml", new ChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }

        #endregion

        #region Common

        /// <summary>
        /// 获取租户名称
        /// </summary>
        /// <returns></returns>
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            { //如果租户Id不为空
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        /// <summary>
        /// 规范返回的Url
        /// </summary>
        /// <param name="returnUrl">返回的Url</param>
        /// <param name="defaultValueBuilder">默认值建造者</param>
        /// <returns></returns>
        private string NormalizeReturnUrl(string returnUrl, Func<string> defaultValueBuilder = null)
        {
            if (defaultValueBuilder == null)
            {
                defaultValueBuilder = GetAppHomeUrl;
            }

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return defaultValueBuilder();
        }

        #endregion

        #region Etc

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                    "App.SimpleMessage",
                    new MessageNotificationData(message),
                    severity: NotificationSeverity.Info,
                    userIds: new[] { defaultTenantAdmin, hostAdmin }
                 );

            return Content("Sent notification: " + message);
        }

        #endregion
    }
}