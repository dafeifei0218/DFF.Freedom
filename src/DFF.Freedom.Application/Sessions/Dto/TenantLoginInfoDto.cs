using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DFF.Freedom.MultiTenancy;

namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 租户登录信息
    /// </summary>
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 租户显示名称
        /// </summary>
        public string Name { get; set; }
    }
}