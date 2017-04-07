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
            //默认链接字符串
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(FreedomConsts.ConnectionStringName);

            //Use database for language management
            //使用数据库管理语言信息
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //设置导航
            Configuration.Navigation.Providers.Add<FreedomNavigationProvider>();

            //设置控制器为Asp.NetCore服务类
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(FreedomApplicationModule).Assembly
                );
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}