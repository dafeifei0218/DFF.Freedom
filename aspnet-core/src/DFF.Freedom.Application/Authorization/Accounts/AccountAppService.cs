using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using DFF.Freedom.Authorization.Accounts.Dto;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization.Accounts
{
    /// <summary>
    /// 账户 应用程序服务
    /// </summary>
    public class AccountAppService : FreedomAppServiceBase, IAccountAppService
    {
        //用户注册管理类
        private readonly UserRegistrationManager _userRegistrationManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRegistrationManager">用户注册管理类</param>
        public AccountAppService(
            UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }

        /// <summary>
        /// 是否租户可用
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}