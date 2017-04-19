using System.Collections.Generic;
using Abp.Localization;

namespace DFF.Freedom.Web.Views.Shared.Components.LanguageSelection
{
    /// <summary>
    /// 语言选择 视图模型
    /// </summary>
    public class LanguageSelectionViewModel
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