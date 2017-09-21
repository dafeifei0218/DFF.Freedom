using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DFF.Freedom.Web.Resources
{
    /// <summary>
    /// ��վ��Դ����
    /// </summary>
    public class WebResourceManager : IWebResourceManager
    {
        private readonly IHostingEnvironment _environment;
        private readonly List<string> _scriptUrls;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="environment">����������</param>
        public WebResourceManager(IHostingEnvironment environment)
        {
            _environment = environment;
            _scriptUrls = new List<string>();
        }

        /// <summary>
        /// ���Script�ű�
        /// </summary>
        /// <param name="url">url����</param>
        /// <param name="addMinifiedOnProd"></param>
        public void AddScript(string url, bool addMinifiedOnProd = true)
        {
            _scriptUrls.AddIfNotContains(NormalizeUrl(url, "js"));
        }

        /// <summary>
        /// ��ȡScript�ű�
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetScripts()
        {
            return _scriptUrls.ToImmutableList();
        }

        /// <summary>
        /// ��ȾScript�ű�
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
        /// �淶��Url
        /// </summary>
        /// <param name="url">Url����</param>
        /// <param name="ext">��׺��</param>
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