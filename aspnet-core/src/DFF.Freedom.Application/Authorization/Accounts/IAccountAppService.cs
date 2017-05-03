using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Authorization.Accounts.Dto;

namespace DFF.Freedom.Authorization.Accounts
{
    /// <summary>
    /// 账户服务
    /// </summary>
    public interface IAccountAppService : IApplicationService
    {
        /// <summary>
        /// 是否租户可用
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
