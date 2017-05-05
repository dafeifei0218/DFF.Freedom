using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace DFF.Freedom.Models.TokenAuth
{
    /// <summary>
    /// 外部认证 模型
    /// </summary>
    public class ExternalAuthenticateModel
    {
        /// <summary>
        /// 认证提供字符串
        /// </summary>
        [Required]
        [MaxLength(UserLogin.MaxLoginProviderLength)]
        public string AuthProvider { get; set; }

        /// <summary>
        /// 提供的键字符串
        /// </summary>
        [Required]
        [MaxLength(UserLogin.MaxProviderKeyLength)]
        public string ProviderKey { get; set; }

        /// <summary>
        /// 提供的访问代码字符串
        /// </summary>
        [Required]
        public string ProviderAccessCode { get; set; }
    }
}
