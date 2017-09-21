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
        /// 多租户是否启用
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }

        /// <summary>
        /// 是否允许自注册
        /// </summary>
        public bool IsSelfRegistrationAllowed { get; set; }

        /// <summary>
        /// 多租户枚举
        /// </summary>
        public MultiTenancySides MultiTenancySide { get; set; }
    }
}