﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.Zero.AspNetCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json.Linq;

namespace DFF.Freedom.Authentication.External.Google
{
    /// <summary>
    /// Google认证提供者Api
    /// </summary>
    public class GoogleAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string Name = "Google";

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        public override async Task<ExternalLoginUserInfo> GetUserInfo(string accessCode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB

                var request = new HttpRequestMessage(HttpMethod.Get, GoogleDefaults.UserInformationEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

                return new ExternalLoginUserInfo
                {
                    Name = GoogleHelper.GetName(payload),
                    EmailAddress = GoogleHelper.GetEmail(payload),
                    Surname = GoogleHelper.GetFamilyName(payload),
                    LoginInfo = new UserLoginInfo(Name, GoogleHelper.GetId(payload))
                };
            }
        }
    }
}
