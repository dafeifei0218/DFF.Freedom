using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// 创建用户 输入模型
    /// </summary>
    [AutoMap(typeof(User))]
    public class CreateUserInput
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}