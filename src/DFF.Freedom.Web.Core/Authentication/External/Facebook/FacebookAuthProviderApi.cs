using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using Abp.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DFF.Freedom.Authentication.External.Facebook
{
    /// <summary>
    /// Facebook认证提供者Api
    /// </summary>
    public class FacebookAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string Name = "Facebook";

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            var endpoint = QueryHelpers.AddQueryString("https://graph.facebook.com/v2.8/me", "access_token", accessCode);
            endpoint = QueryHelpers.AddQueryString(endpoint, "appsecret_proof", GenerateAppSecretProof(accessCode));
            endpoint = QueryHelpers.AddQueryString(endpoint, "fields", "email,last_name,first_name,middle_name");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                client.DefaultRequestHeaders.Host = "graph.facebook.com";
                client.Timeout = TimeSpan.FromSeconds(30);
                client.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB

                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

                var name = FacebookHelper.GetFirstName(payload);
                var middleName = FacebookHelper.GetMiddleName(payload);
                if (!middleName.IsNullOrEmpty())
                {
                    name += middleName;
                }

                return new ExternalAuthUserInfo
                {
                    Name = name,
                    EmailAddress = FacebookHelper.GetEmail(payload),
                    Surname = FacebookHelper.GetLastName(payload),
                    Provider = Name,
                    ProviderKey = FacebookHelper.GetId(payload)
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        private string GenerateAppSecretProof(string accessToken)
        {
            using (var algorithm = new HMACSHA256(Encoding.ASCII.GetBytes(ProviderInfo.ClientSecret)))
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
                var builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
                }
                return builder.ToString();
            }
        }
    }
}
