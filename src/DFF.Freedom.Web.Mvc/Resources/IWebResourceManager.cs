using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DFF.Freedom.Web.Resources
{
    /// <summary>
    /// 网站资源管理接口
    /// </summary>
    public interface IWebResourceManager
    {
        /// <summary>
        /// 添加Script脚本
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addMinifiedOnProd"></param>
        void AddScript(string url, bool addMinifiedOnProd = true);

        /// <summary>
        /// 获取Script脚本
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<string> GetScripts();

        /// <summary>
        /// 渲染Script脚本
        /// </summary>
        /// <returns></returns>
        HelperResult RenderScripts();
    }
}
