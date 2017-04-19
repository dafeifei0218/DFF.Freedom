using Abp.Application.Navigation;

namespace DFF.Freedom.Web.Views.Shared.Components.TopMenu
{
    /// <summary>
    /// 顶部菜单 视图模型
    /// </summary>
    public class TopMenuViewModel
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