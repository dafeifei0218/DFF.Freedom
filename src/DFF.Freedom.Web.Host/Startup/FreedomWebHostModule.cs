using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DFF.Freedom.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DFF.Freedom.Web.Host.Startup
{
    /// <summary>
    /// WebHost模块
    /// </summary>
    [DependsOn(
       typeof(FreedomWebCoreModule))]
    public class FreedomWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env">宿主环境</param>
        public FreedomWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FreedomWebHostModule).GetAssembly());
        }
    }
}
