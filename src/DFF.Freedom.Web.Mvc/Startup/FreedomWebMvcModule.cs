using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DFF.Freedom.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DFF.Freedom.Web.Startup
{
    /// <summary>
    /// WebMvc模块
    /// </summary>
    [DependsOn(typeof(FreedomWebCoreModule))]
    public class FreedomWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public FreedomWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 预初始化
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<FreedomNavigationProvider>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FreedomWebMvcModule).GetAssembly());
        }
    }
}