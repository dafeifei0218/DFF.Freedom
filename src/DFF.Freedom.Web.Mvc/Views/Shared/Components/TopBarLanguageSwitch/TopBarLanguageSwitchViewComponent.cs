using System.Linq;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.TopBarLanguageSwitch
{
    /// <summary>
    /// 顶部栏语言切换 视图组件
    /// </summary>
    public class TopBarLanguageSwitchViewComponent : FreedomViewComponent
    {
        private readonly ILanguageManager _languageManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageManager"></param>
        public TopBarLanguageSwitchViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var model = new TopBarLanguageSwitchViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
            };

            return View(model);
        }
    }
}
