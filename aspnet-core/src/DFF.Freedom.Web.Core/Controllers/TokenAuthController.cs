using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using Abp.Zero.AspNetCore;
using DFF.Freedom.Authentication.External;
using DFF.Freedom.Authentication.JwtBearer;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Models.TokenAuth;
using DFF.Freedom.MultiTenancy;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Controllers
{
    /// <summary>
    /// 令牌认证 控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : FreedomControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logInManager">登录管理类</param>
        /// <param name="tenantCache">租户缓存</param>
        /// <param name="abpLoginResultTypeHelper">Abp登录结果类型帮助类</param>
        /// <param name="configuration">令牌认证配置</param>
        /// <param name="externalAuthConfiguration">外部认证配置</param>
        /// <param name="externalAuthManager">外部认证管理</param>
        /// <param name="userRegistrationManager">用户注册管理</param>
        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="model">认证模型</param>
        /// <returns>认证结果模型</returns>
        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull()
            );

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
            };
        }

        /// <summary>
        /// 获取外部认证提供信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return _externalAuthConfiguration.Providers.MapTo<List<ExternalLoginProviderInfoModel>>();
        }

        /// <summary>
        /// 外部认证
        /// </summary>
        /// <param name="model">外部认证模型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        //Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        /// <summary>
        /// 注册外部用户 异步方法
        /// </summary>
        /// <param name="externalUser">外部登录用户信息</param>
        /// <returns></returns>
        private async Task<User> RegisterExternalUserAsync(ExternalLoginUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.LoginInfo.LoginProvider,
                    ProviderKey = externalUser.LoginInfo.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// 获取外部用户信息
        /// </summary>
        /// <param name="model">外部认证模型</param>
        /// <returns></returns>
        private async Task<ExternalLoginUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.LoginInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        /// <summary>
        /// 获取租户名称或空
        /// </summary>
        /// <returns></returns>
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        /// <summary>
        /// 获取登录结果 异步方法
        /// </summary>
        /// <param name="usernameOrEmailAddress">用户名或邮件地址</param>
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

        /// <summary>
        /// 创建访问令牌
        /// </summary>
        /// <param name="claims">表示声明</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// 创建声明
        /// </summary>
        /// <param name="identity">委托基于声明的标识。</param>
        /// <returns></returns>
        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        /// <summary>
        /// 获取加密的访问令牌
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}
