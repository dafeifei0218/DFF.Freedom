using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.MultiTenancy.Dto;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户 应用程序服务接口
    /// </summary>
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
