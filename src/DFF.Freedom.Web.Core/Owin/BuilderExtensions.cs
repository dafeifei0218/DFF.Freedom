#if FEATURE_SIGNALR 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Owin.Builder;
using Owin;

namespace DFF.Freedom.Owin
{
    /// <summary>
    /// This class (UseAppBuilder method) integrates OWIN pipeline to ASP.NET Core pipeline and
    /// allows us to use Owin based middlewares in ASP.NET Core applications.
    /// 建造者 扩展类。
    /// 这个类（UseAppBuilder方法）将用于管道ASP.NET Core管道，
    /// 让我们在ASP.NET Core应用程序使用基于中间件技术。
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// 使用应用程序建造者
        /// </summary>
        /// <param name="app">应用程序建造者</param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAppBuilder(
            this IApplicationBuilder app,
            Action<IAppBuilder> configure)
        {
            app.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var appBuilder = new AppBuilder();
                    appBuilder.Properties["builder.DefaultApp"] = next;

                    configure(appBuilder);

                    return appBuilder.Build<Func<IDictionary<string, object>, Task>>();
                });
            });

            return app;
        }
    }
}
#endif