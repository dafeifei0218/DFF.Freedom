using System.Collections.Generic;
using Abp.Localization;
using Microsoft.AspNetCore.Http;

namespace DFF.Freedom.Web.Views.Shared.Components.AccountLanguages
{
    /// <summary>
    /// 语言选项 视图模型
    /// </summary>
    public class LanguageSelectionViewModel
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public LanguageInfo CurrentLanguage { get; set; }

        /// <summary>
        /// 语言信息列表
        /// </summary>
        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        /// <summary>
        /// 当前Url
        /// </summary>
        public PathString CurrentUrl { get; set; }
    }
}