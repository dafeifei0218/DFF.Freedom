using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Security;
using DFF.Freedom.Authentication.External;
using DFF.Freedom.Authentication.External.Facebook;
using DFF.Freedom.Authentication.External.Google;
using DFF.Freedom.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DFF.Freedom.Web.Host.Startup
{
    /// <summary>
    /// 认证配置器
    /// </summary>
    public static class AuthConfigurer
    {
        /// <summary>
        /// Configures the specified application.
        /// 配置指定的应用程序。
        /// </summary>
        /// <param name="app">The application. 应用程序</param>
        /// <param name="configuration">The configuration. 配置接口</param>
        public static void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                app.UseJwtBearerAuthentication(CreateJwtBearerAuthenticationOptions(app));
            }

            var externalAuthConfiguration = app.ApplicationServices.GetRequiredService<ExternalAuthConfiguration>();

            if (bool.Parse(configuration["Authentication:Facebook:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        FacebookAuthProviderApi.Name,
                        configuration["Authentication:Facebook:AppId"],
                        configuration["Authentication:Facebook:AppSecret"],
                        typeof(FacebookAuthProviderApi)
                    )
                );
            }

            if (bool.Parse(configuration["Authentication:Google:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        GoogleAuthProviderApi.Name,
                        configuration["Authentication:Google:ClientId"],
                        configuration["Authentication:Google:ClientSecret"],
                        typeof(GoogleAuthProviderApi)
                    )
                );
            }
        }

        /// <summary>
        /// 创建Jwt认证选项
        /// </summary>
        /// <param name="app">应用程序建造者</param>
        /// <returns></returns>
        private static JwtBearerOptions CreateJwtBearerAuthenticationOptions(IApplicationBuilder app)
        {
            var tokenAuthConfig = app.ApplicationServices.GetRequiredService<TokenAuthConfiguration>();
            return new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenAuthConfig.SecurityKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = tokenAuthConfig.Issuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = tokenAuthConfig.Audience,

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                },

                Events = new JwtBearerEvents
                {
                    OnMessageReceived = QueryStringTokenResolver
                }
            };
        }

        /* This method is needed to authorize SignalR javascript client.
         * SignalR can not send authorization header. So, we are getting it from query string as an encrypted text. */
        //这种方法需要授权SignalR JavaScript客户端。
        //SignalR不能发送授权头。所以，我们将它从查询字符串作为加密文本。
        /// <summary>
        /// 参数令牌解析
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                //We are just looking for signalr clients
                //我们只是在寻找SignalR客户端
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                //Cookie value does not matches to querystring value
                //Cookie的值不匹配的参数的值
                return Task.CompletedTask;
            }

            //Set auth token from cookie
            //从Cookie设置身份认证令牌
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, AppConsts.DefaultPassPhrase);
            return Task.CompletedTask;
        }
    }
}
