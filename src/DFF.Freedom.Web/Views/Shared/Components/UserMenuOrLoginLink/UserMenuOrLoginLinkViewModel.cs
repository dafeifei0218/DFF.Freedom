using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Web.Views.Shared.Components.UserMenuOrLoginLink
{
    /// <summary>
    /// 用户菜单或登录连接 视图模型
    /// </summary>
    public class UserMenuOrLoginLinkViewModel
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }
        /// <summary>
        /// 是否启用多租户
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }

        /// <summary>
        /// 获取显示登录名
        /// </summary>
        /// <returns></returns>
        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }
    }
}