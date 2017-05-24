﻿using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.Owin;
using DFF.Freedom.Configuration;
using DFF.Freedom.Owin;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Cors;
using Owin;


namespace DFF.Freedom.Web.Host.Startup
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
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
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
            });

            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, p =>
                {
                    //todo: Get from confiuration
                    p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen();

            //Configure Abp and Dependency Injection
            return services.AddAbp<FreedomWebHostModule>(options =>
            {
                //Configure Log4Net logging
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
            app.UseAbp(); //Initializes ABP framework. 初始化ABP框架。

            app.UseCors(DefaultCorsPolicyName); //Enable CORS! 启用CORS

            AuthConfigurer.Configure(app, _appConfiguration);

            app.UseStaticFiles();

            //Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);

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
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi(); //URL: /swagger/ui
        }

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
    }
}
