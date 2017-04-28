using System;
using System.Reflection;
using System.Text;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Web.SignalR;
using Abp.Zero.AspNetCore;
using Abp.Zero.Configuration;
using DFF.Freedom.Authentication.JwtBearer;
using DFF.Freedom.Configuration;
using DFF.Freedom.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DFF.Freedom
{
    [DependsOn(
         typeof(FreedomApplicationModule),
         typeof(FreedomEntityFrameworkModule),
         typeof(AbpAspNetCoreModule),
         typeof(AbpZeroAspNetCoreModule),
         typeof(AbpWebSignalRModule)
     )]
    public class FreedomWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public FreedomWebModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
            //默认链接字符串
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                FreedomConsts.ConnectionStringName
            );

            //Use database for language management
            //使用数据库管理语言信息
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(FreedomApplicationModule).Assembly
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
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
