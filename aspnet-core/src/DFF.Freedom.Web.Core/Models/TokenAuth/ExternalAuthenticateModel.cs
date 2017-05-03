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
        /// 
        /// </summary>
        [Required]
        [MaxLength(UserLogin.MaxLoginProviderLength)]
        public string AuthProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(UserLogin.MaxProviderKeyLength)]
        public string ProviderKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ProviderAccessCode { get; set; }
    }
}
