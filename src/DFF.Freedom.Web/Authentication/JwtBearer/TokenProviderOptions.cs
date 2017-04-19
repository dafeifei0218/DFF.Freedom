using System;
using Microsoft.IdentityModel.Tokens;

namespace DFF.Freedom.Web.Authentication.JwtBearer
{
    /// <summary>
    /// Token提供者选项
    /// </summary>
    public class TokenProviderOptions
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 观众
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// 签名证书
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}