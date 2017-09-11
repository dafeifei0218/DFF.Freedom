using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Authorization.Users;
using Abp.Extensions;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 注册 视图模型
    /// </summary>
    public class RegisterViewModel : IValidatableObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
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
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// 是否外部登陆
        /// </summary>
        public bool IsExternalLogin { get; set; }

        /// <summary>
        /// 外部登录认证模式
        /// </summary>
        public string ExternalLoginAuthSchema { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="validationContext">验证上下文</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                var emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                if (!UserName.Equals(EmailAddress) && emailRegex.IsMatch(UserName))
                {
                    //用户名不能是电子邮件地址，除非与您的电子邮件地址相同！
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
    }
}