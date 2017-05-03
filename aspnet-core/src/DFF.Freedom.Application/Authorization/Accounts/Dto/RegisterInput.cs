using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Validation;
using Abp.Extensions;

namespace DFF.Freedom.Authorization.Accounts.Dto
{
    /// <summary>
    /// 注册 输入模型
    /// </summary>
    public class RegisterInput : IValidatableObject
    {
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
        /// 用户名称
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

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
        /// 验证码响应
        /// </summary>
        [DisableAuditing]
        public string CaptchaResponse { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="validationContext">验证上下文</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                if (!UserName.Equals(EmailAddress) && ValidationHelper.IsEmail(UserName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
    }
}
