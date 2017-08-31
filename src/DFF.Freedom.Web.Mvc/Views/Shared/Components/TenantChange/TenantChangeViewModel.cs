using Abp.AutoMapper;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Web.Views.Shared.Components.TenantChange
{
    /// <summary>
    /// �⻧���� ��ͼģ��
    /// </summary>
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}