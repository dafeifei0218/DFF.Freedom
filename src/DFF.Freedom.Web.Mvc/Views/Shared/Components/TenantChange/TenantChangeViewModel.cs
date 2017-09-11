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
        /// <summary>
        /// �⻧��¼��Ϣ
        /// </summary>
        public TenantLoginInfoDto Tenant { get; set; }
    }
}