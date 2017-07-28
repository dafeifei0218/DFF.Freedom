using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.MultiTenancy.Dto;

namespace DFF.Freedom.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
