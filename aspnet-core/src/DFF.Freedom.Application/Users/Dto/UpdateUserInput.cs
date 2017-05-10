using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;
using Abp.Authorization.Users;
using Abp.Auditing;

namespace DFF.Freedom.Users.Dto
{
    [AutoMap(typeof(User))]
    public class UpdateUserInput
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
