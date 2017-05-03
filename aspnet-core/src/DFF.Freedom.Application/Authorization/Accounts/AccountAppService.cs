using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using DFF.Freedom.Authorization.Accounts.Dto;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization.Accounts
{
    /// <summary>
    /// �˻�����
    /// </summary>
    public class AccountAppService : FreedomAppServiceBase, IAccountAppService
    {
        //�û�ע�������
        private readonly UserRegistrationManager _userRegistrationManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userRegistrationManager"></param>
        public AccountAppService(
            UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }

        /// <summary>
        /// �Ƿ��⻧����
        /// </summary>
        /// <param name="input">����ģ��</param>
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
        /// ע��
        /// </summary>
        /// <param name="input">����ģ��</param>
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