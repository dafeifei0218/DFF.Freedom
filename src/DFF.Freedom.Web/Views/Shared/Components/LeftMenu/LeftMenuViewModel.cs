using Abp.Application.Navigation;

namespace DFF.Freedom.Web.Views.Shared.Components.LeftMenu
{
    /// <summary>
    /// 左侧菜单 视图模型
    /// </summary>
    public class LeftMenuViewModel
    {
        /// <summary>
        /// 主菜单
        /// </summary>
        public UserMenu MainMenu { get; set; }

        /// <summary>
        /// 激活菜单名称
        /// </summary>
        public string ActiveMenuItemName { get; set; }
    }
}
