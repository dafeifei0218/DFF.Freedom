using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users;

namespace DFF.Freedom.MultiTenancy.Dto
{
    /// <summary>
    /// �����⻧ ����ģ��
    /// </summary>
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantInput
    {
        /// <summary>
        /// �⻧����
        /// </summary>
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// ����Ա�ĵ����ʼ���ַ
        /// </summary>
        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        /// <summary>
        /// �����ַ���
        /// </summary>
        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
    }
}