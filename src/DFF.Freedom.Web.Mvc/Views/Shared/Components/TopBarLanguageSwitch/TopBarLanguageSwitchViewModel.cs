using System.Collections.Generic;
using Abp.Localization;

namespace DFF.Freedom.Web.Views.Shared.Components.TopBarLanguageSwitch
{
    /// <summary>
    /// 顶部栏语言切换 视图模型
    /// </summary>
    public class TopBarLanguageSwitchViewModel
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public LanguageInfo CurrentLanguage { get; set; }

        /// <summary>
        /// 语言列表
        /// </summary>
        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }
}