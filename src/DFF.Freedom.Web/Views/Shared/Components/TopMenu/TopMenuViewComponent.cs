using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.TopMenu
{
    /// <summary>
    /// 顶部菜单视图组件
    /// </summary>
    public class TopMenuViewComponent : ViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userNavigationManager">用户导航管理者</param>
        /// <param name="abpSession">Abp会话</param>
        public TopMenuViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
        }

        /// <summary>
        /// 调用异步方法
        /// </summary>
        /// <param name="activeMenu">激活的菜单</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string activeMenu = "")
        {
            var model = new TopMenuViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveMenuItemName = activeMenu
            };

            return View(model);
        }
    }
}
