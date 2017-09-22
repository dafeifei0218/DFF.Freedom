using System;
using System.Linq;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using DFF.Freedom.Configuration;
using DFF.Freedom.Identity;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Abp.Extensions;

#if FEATURE_SIGNALR
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using DFF.Freedom.Owin;
using Abp.Owin;
#endif


namespace DFF.Freedom.Web.Host.Startup
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
        //默认CORS策略名称
        private const string DefaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env">宿主环境</param>
        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services">服务集合接口</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            //MVC过滤器设置
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
            });

            IdentityRegistrar.Register(services);

            //Configure CORS for angular2 UI
            //为Angular2 UI配置CORS
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    //应用：CorsOrigins在appsettings.json可以包含多个地址，用逗号分割。
                    builder
                        .WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            //Swagger - 在配置方法中启用这一行和相关行，以便启用swagger UI。
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Freedom API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });

            //Configure Abp and Dependency Injection
            //配置Abp和依赖注入
            return services.AddAbp<FreedomWebHostModule>(options =>
            {
                //Configure Log4Net logging
                //配置Log4Net日志
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app">应用程序建造者</param>
        /// <param name="env">宿主环境</param>
        /// <param name="loggerFactory">日志工厂</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework. 初始化ABP框架

            app.UseCors(DefaultCorsPolicyName); //Enable CORS! 启用CORS！

            AuthConfigurer.Configure(app, _appConfiguration);

            //对静态文件的访问
            app.UseStaticFiles();

#if FEATURE_SIGNALR
            //Integrate to OWIN
            //整合OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            // 启用中间件服务生成Swagger作为JSON终点
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
			// 启用中间件服务swagger-ui资产（HTML，JS，CSS等）
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Freedom API V1");
            }); //URL: /swagger
        }

#if FEATURE_SIGNALR

        /// <summary>
        /// 配置Owin服务
        /// </summary>
        /// <param name="app">应用程序建造者</param>
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            app.Properties["host.AppName"] = "AbpZeroTemplate";

            app.UseAbp();
            
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
#endif
    }
}
