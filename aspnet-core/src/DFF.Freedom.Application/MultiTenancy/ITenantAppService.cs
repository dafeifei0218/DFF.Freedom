using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.MultiTenancy.Dto;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户 应用程序服务接口
    /// </summary>
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 获取租户列表
        /// </summary>
        /// <returns>租户列表</returns>
        ListResultDto<TenantListDto> GetTenants();

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        Task CreateTenant(CreateTenantInput input);
    }
}
