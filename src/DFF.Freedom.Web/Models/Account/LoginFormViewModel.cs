namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 登录表单 视图模型
    /// </summary>
    public class LoginFormViewModel
    {
        /// <summary>
        /// 返回链接
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 是否启用多租户
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }
    }
}