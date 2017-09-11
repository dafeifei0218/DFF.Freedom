using Abp.MultiTenancy;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 登录表单 视图模型
    /// </summary>
    public class LoginFormViewModel
    {
        /// <summary>
        /// 返回Url
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelfRegistrationAllowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MultiTenancySides MultiTenancySide { get; set; }
    }
}