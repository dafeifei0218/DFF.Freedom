using System.Linq;
using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Views.Shared.Components.AccountLanguages
{
    /// <summary>
    /// 账户语言 视图组件
    /// </summary>
    public class AccountLanguagesViewComponent : FreedomViewComponent
    {
        private readonly ILanguageManager _languageManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="languageManager">语言管理</param>
        public AccountLanguagesViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns></returns>
        public Task<IViewComponentResult> InvokeAsync()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
                    .Where(l => !l.IsDisabled).ToList()
                    .Where(l => !l.IsDisabled)
                    .ToList(),
                CurrentUrl = Request.Path
            };

            return Task.FromResult(View(model) as IViewComponentResult);
        }
    }
}
