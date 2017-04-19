using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Users;
using Abp.Extensions;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterViewModel : IValidatableObject
    {
        /// <summary>
        /// Not required for single-tenant applications.
        /// </summary>
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExternalLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExternalLoginAuthSchema { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                var emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                if (!UserName.Equals(EmailAddress) && emailRegex.IsMatch(UserName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
    }
}