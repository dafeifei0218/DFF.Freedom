using System;
using Microsoft.IdentityModel.Tokens;

namespace DFF.Freedom.Authentication.JwtBearer
{
    /// <summary>
    /// 令牌认证配置
    /// </summary>
    public class TokenAuthConfiguration
    {
        /// <summary>
        /// 安全秘钥
        /// </summary>
        public SymmetricSecurityKey SecurityKey { get; set; }

        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 观众
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 签名证书
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
    }
}
