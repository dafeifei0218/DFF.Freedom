using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using DFF.Freedom.Configuration;
using DFF.Freedom.Configuration.Ui;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.RightSideBar
{
    public class RightSideBarViewComponent : FreedomViewComponent
    {
        private readonly ISettingManager _settingManager;

        public RightSideBarViewComponent(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var themeName = await _settingManager.GetSettingValueAsync(AppSettingNames.UiTheme);

            var viewModel = new RightSideBarViewModel
            {
                CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            };

            return View(viewModel);
        }
    }
}
