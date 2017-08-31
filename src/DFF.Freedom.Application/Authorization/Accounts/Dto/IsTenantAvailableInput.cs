using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace DFF.Freedom.Authorization.Accounts.Dto
{
    /// <summary>
    /// 是否租户可用 输入模型
    /// </summary>
    public class IsTenantAvailableInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}

