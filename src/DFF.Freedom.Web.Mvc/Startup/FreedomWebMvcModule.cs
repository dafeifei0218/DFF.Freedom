using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DFF.Freedom.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DFF.Freedom.Web.Startup
{
    [DependsOn(typeof(FreedomWebCoreModule))]
    public class FreedomWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FreedomWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<FreedomNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FreedomWebMvcModule).GetAssembly());
        }
    }
}