using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.LanguageSelection
{
    /// <summary>
    /// 语言选择视图
    /// </summary>
    public class LanguageSelectionViewComponent : ViewComponent
    {
        private readonly ILanguageManager _languageManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageManager">语言管理者</param>
        public LanguageSelectionViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
            };

            return View(model);
        }
    }
}
