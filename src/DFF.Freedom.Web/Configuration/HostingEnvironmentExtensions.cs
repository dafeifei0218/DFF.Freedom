using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DFF.Freedom.Configuration;

namespace DFF.Freedom.Web.Configuration
{
    /// <summary>
    /// 宿主环境 扩展类
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// 获取应用程序配置
        /// </summary>
        /// <param name="env">宿主环境</param>
        /// <returns></returns>
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}