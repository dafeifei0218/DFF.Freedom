using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace DFF.Freedom.MultiTenancy.Dto
{
    /// <summary>
    /// �⻧�б� ���ݴ������
    /// </summary>
    [AutoMapFrom(typeof(Tenant))]
    public class TenantListDto : EntityDto
    {
        /// <summary>
        /// �⻧����
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
    }
}