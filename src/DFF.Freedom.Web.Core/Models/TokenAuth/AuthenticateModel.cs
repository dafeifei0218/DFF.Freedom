using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Models.TokenAuth
{
    /// <summary>
    /// 认证 模型
    /// </summary>
    public class AuthenticateModel
    {
        /// <summary>
        /// 用户名或邮件地址
        /// </summary>
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }
        
        /// <summary>
        /// 是否记住客户端
        /// </summary>
        public bool RememberClient { get; set; }
    }
}
