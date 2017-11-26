using Abp.Application.Navigation;

namespace DFF.Freedom.Web.Views.Shared.Components.SideBarNav
{
    /// <summary>
    /// 侧栏导航 视图模型
    /// </summary>
    public class SideBarNavViewModel
    {
        /// <summary>
        /// 用户菜单
        /// </summary>
        public UserMenu MainMenu { get; set; }

        /// <summary>
        /// 激活菜单项目名称
        /// </summary>
        public string ActiveMenuItemName { get; set; }
    }
}
