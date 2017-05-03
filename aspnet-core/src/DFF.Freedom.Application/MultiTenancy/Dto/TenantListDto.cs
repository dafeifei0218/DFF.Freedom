using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace DFF.Freedom.MultiTenancy.Dto
{
    /// <summary>
    /// 租户列表 数据传输对象
    /// </summary>
    [AutoMapFrom(typeof(Tenant))]
    public class TenantListDto : EntityDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}