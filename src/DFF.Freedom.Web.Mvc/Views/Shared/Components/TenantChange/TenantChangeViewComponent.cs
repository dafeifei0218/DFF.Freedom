using System.Threading.Tasks;
using Abp.AutoMapper;
using DFF.Freedom.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.TenantChange
{
    /// <summary>
    /// 租户更改 试图组件
    /// </summary>
    public class TenantChangeViewComponent : FreedomViewComponent
    {
        private readonly ISessionAppService _sessionAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sessionAppService">Session应用程序服务接口</param>
        public TenantChangeViewComponent(ISessionAppService sessionAppService)
        {
            _sessionAppService = sessionAppService;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            var model = loginInfo.MapTo<TenantChangeViewModel>();
            return View(model);
        }
    }
}
