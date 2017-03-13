using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.MultiTenancy.Dto;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 获取租户列表
        /// </summary>
        /// <returns></returns>
        ListResultDto<TenantListDto> GetTenants();

        /// <summary>
        /// 创建租主
        /// </summary>
        /// <param name="input">创建租户输入模型</param>
        /// <returns></returns>
        Task CreateTenant(CreateTenantInput input);
    }
}
