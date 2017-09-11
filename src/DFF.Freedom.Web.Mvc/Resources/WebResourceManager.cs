using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DFF.Freedom.Web.Resources
{
    /// <summary>
    /// 网站资源管理
    /// </summary>
    public class WebResourceManager : IWebResourceManager
    {
        private readonly IHostingEnvironment _environment;
        private readonly List<string> _scriptUrls;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="environment"></param>
        public WebResourceManager(IHostingEnvironment environment)
        {
            _environment = environment;
            _scriptUrls = new List<string>();
        }

        /// <summary>
        /// 添加Script脚本
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addMinifiedOnProd"></param>
        public void AddScript(string url, bool addMinifiedOnProd = true)
        {
            _scriptUrls.AddIfNotContains(NormalizeUrl(url, "js"));
        }

        /// <summary>
        /// 获取Script脚本
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetScripts()
        {
            return _scriptUrls.ToImmutableList();
        }

        /// <summary>
        /// 渲染Script脚本
        /// </summary>
        /// <returns></returns>
        public HelperResult RenderScripts()
        {
            return new HelperResult(async writer =>
            {
                foreach (var scriptUrl in _scriptUrls)
                {
                    await writer.WriteAsync($"<script src=\"{scriptUrl}\" asp-append-version=\"true\"></script>");
                }
            });
        }

        /// <summary>
        /// 规范的Url
        /// </summary>
        /// <param name="url">Url连接</param>
        /// <param name="ext">后缀</param>
        /// <returns></returns>
        private string NormalizeUrl(string url, string ext)
        {
            if (_environment.IsDevelopment())
            {
                return url;
            }

            if (url.EndsWith(".min." + ext))
            {
                return url;
            }

            return url.Left(url.Length - ext.Length) + "min." + ext;
        }
    }
}