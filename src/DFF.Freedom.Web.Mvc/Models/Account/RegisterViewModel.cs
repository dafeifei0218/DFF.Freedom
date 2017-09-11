using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Abp.Authorization.Users;
using Abp.Extensions;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// ע�� ��ͼģ��
    /// </summary>
    public class RegisterViewModel : IValidatableObject
    {
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// �Ƿ��ⲿ��½
        /// </summary>
        public bool IsExternalLogin { get; set; }

        /// <summary>
        /// �ⲿ��¼��֤ģʽ
        /// </summary>
        public string ExternalLoginAuthSchema { get; set; }

        /// <summary>
        /// ��֤
        /// </summary>
        /// <param name="validationContext">��֤������</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserName.IsNullOrEmpty())
            {
                var emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                if (!UserName.Equals(EmailAddress) && emailRegex.IsMatch(UserName))
                {
                    //�û��������ǵ����ʼ���ַ�����������ĵ����ʼ���ַ��ͬ��
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
    }
}