using Abp.AutoMapper;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Web.Views.Shared.Components.TenantChange
{
    /// <summary>
    /// 租户更改 视图模型
    /// </summary>
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        /// <summary>
        /// 租户登录信息
        /// </summary>
        public TenantLoginInfoDto Tenant { get; set; }
    }
}