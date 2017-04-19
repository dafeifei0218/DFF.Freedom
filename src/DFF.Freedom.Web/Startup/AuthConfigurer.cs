using System;
using System.Text;
using DFF.Freedom.Web.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DFF.Freedom.Web.Startup
{
    /// <summary>
    /// 验证配置器
    /// </summary>
    public static class AuthConfigurer
    {
        /// <summary>
        /// 认证方案
        /// </summary>
        public const string AuthenticationScheme = "FreedomAuthSchema";

        /// <summary>
        /// Configures the specified application.
        /// 配置指定的应用程序。
        /// </summary>
        /// <param name="app">The application. 应用程序</param>
        /// <param name="configuration">The configuration. 配置接口</param>
        public static void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = AuthenticationScheme,
                LoginPath = new PathString("/Account/Login/"),
                AccessDeniedPath = new PathString("/Error/E403"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            if (bool.Parse(configuration["Authentication:Microsoft:IsEnabled"]))
            {
                app.UseMicrosoftAccountAuthentication(CreateMicrosoftAuthOptions(configuration));
            }

            if (bool.Parse(configuration["Authentication:Google:IsEnabled"]))
            {
                app.UseGoogleAuthentication(CreateGoogleAuthOptions(configuration));
            }

            if (bool.Parse(configuration["Authentication:Twitter:IsEnabled"]))
            {
                app.UseTwitterAuthentication(CreateTwitterAuthOptions(configuration));
            }

            if (bool.Parse(configuration["Authentication:Facebook:IsEnabled"]))
            {
                app.UseFacebookAuthentication(CreateFacebookAuthOptions(configuration));
            }

            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                ConfigureJwtBearerAuthentication(app, configuration);
            }
        }

        /// <summary>
        /// 创建微软认证选项
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <returns></returns>
        private static MicrosoftAccountOptions CreateMicrosoftAuthOptions(IConfiguration configuration)
        {
            return new MicrosoftAccountOptions
            {
                SignInScheme = AuthenticationScheme,
                ClientId = configuration["Authentication:Microsoft:ClientId"],
                ClientSecret = configuration["Authentication:Microsoft:ClientSecret"]
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <returns></returns>
        private static GoogleOptions CreateGoogleAuthOptions(IConfiguration configuration)
        {
            return new GoogleOptions
            {
                SignInScheme = AuthenticationScheme,
                ClientId = configuration["Authentication:Google:ClientId"],
                ClientSecret = configuration["Authentication:Google:ClientSecret"]
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <returns></returns>
        private static TwitterOptions CreateTwitterAuthOptions(IConfiguration configuration)
        {
            return new TwitterOptions
            {
                SignInScheme = AuthenticationScheme,
                ConsumerKey = configuration["Authentication:Twitter:ConsumerKey"],
                ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"],
                RetrieveUserDetails = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration">配置接口</param>
        /// <returns></returns>
        private static FacebookOptions CreateFacebookAuthOptions(IConfiguration configuration)
        {
            var options = new FacebookOptions
            {
                AppId = configuration["Authentication:Facebook:AppId"],
                AppSecret = configuration["Authentication:Facebook:AppSecret"],
                SignInScheme = AuthenticationScheme
            };

            options.Scope.Add("email");
            options.Scope.Add("public_profile");

            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration">配置接口</param>
        private static void ConfigureJwtBearerAuthentication(IApplicationBuilder app, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"]));

            //Adding bearer authentication
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                }
            });

            // Adding JWT generation endpoint
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(new TokenProviderOptions
            {
                Path = "/jwt-token/authenticate",
                Issuer = configuration["Authentication:JwtBearer:Issuer"],
                Audience = configuration["Authentication:JwtBearer:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(1)
            }));
        }
    }
}
