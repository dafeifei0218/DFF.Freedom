using System.Reflection;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using DFF.Freedom.Configuration;
using DFF.Freedom.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Zero.Configuration;

namespace DFF.Freedom.Web.Startup
{
    /// <summary>
    /// Web模块
    /// </summary>
    [DependsOn(
        typeof(FreedomApplicationModule), 
        typeof(FreedomEntityFrameworkModule), 
        typeof(AbpAspNetCoreModule))]
    public class FreedomWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public FreedomWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FreedomConsts.ConnectionStringName);

            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Navigation.Providers.Add<FreedomNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(FreedomApplicationModule).Assembly
                );
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}