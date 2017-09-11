using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 登录表单 视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名或邮件地址
        /// </summary>
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住我
        /// </summary>
        public bool RememberMe { get; set; }
    }
}