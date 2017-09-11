using System.Threading.Tasks;
using Abp.Configuration.Startup;
using DFF.Freedom.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.SideBarUserArea
{
    /// <summary>
    /// 侧栏用户区域 视图组件
    /// </summary>
    public class SideBarUserAreaViewComponent : FreedomViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sessionAppService">Session应用程序服务接口</param>
        /// <param name="multiTenancyConfig">多租户配置接口</param>
        public SideBarUserAreaViewComponent(ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return View(model);
        }
    }
}
