using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Web.Views.Shared.Components.SideBarUserArea
{
    /// <summary>
    /// 侧栏用户区域 视图模型
    /// </summary>
    public class SideBarUserAreaViewModel
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
        /// 获取显示登录用户名
        /// </summary>
        /// <returns></returns>
        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            if (!IsMultiTenancyEnabled)
            { //如果未启用多租户
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }
    }
}
