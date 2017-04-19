using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 登录 视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 用户或邮箱地址
        /// </summary>
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }
    }
}