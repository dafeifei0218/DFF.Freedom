using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users;

namespace DFF.Freedom.MultiTenancy.Dto
{
    /// <summary>
    /// 创建租户 输入模型
    /// </summary>
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 管理员的电子邮件地址
        /// </summary>
        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
    }
}