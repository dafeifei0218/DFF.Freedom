using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.SideBarNav
{
    /// <summary>
    /// 侧栏导航 视图组件
    /// </summary>
    public class SideBarNavViewComponent : FreedomViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userNavigationManager">用户导航管理类</param>
        /// <param name="abpSession"></param>
        public SideBarNavViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
        }

        /// <summary>
        /// 调用 异步方法
        /// </summary>
        /// <param name="activeMenu">激活的菜单</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveMenuItemName = activeMenu
            };

            return View(model);
        }
    }
}
