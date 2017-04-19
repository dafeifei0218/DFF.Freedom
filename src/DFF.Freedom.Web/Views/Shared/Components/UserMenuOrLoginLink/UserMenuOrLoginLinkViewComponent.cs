using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using DFF.Freedom.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.UserMenuOrLoginLink
{
    /// <summary>
    /// 用户菜单或登录连接视图组件
    /// </summary>
    public class UserMenuOrLoginLinkViewComponent : ViewComponent
    {
        private readonly IAbpSession _abpSession;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abpSession">Abp会话</param>
        /// <param name="sessionAppService">会话服务</param>
        /// <param name="multiTenancyConfig">多租户配置</param>
        public UserMenuOrLoginLinkViewComponent(
            IAbpSession abpSession, 
            ISessionAppService sessionAppService, 
            IMultiTenancyConfig multiTenancyConfig)
        {
            _abpSession = abpSession;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        /// <summary>
        /// 调用异步方法
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserMenuOrLoginLinkViewModel model;

            if (_abpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                };
            }

            return View(model);
        }
    }
}
