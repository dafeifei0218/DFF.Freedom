using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部登录提供者信息
    /// </summary>
    public class ExternalLoginProviderInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端密钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 提供者Api类型
        /// </summary>
        public Type ProviderApiType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="clientId">客户端Id</param>
        /// <param name="clientSecret">客户端密钥</param>
        /// <param name="providerApiType">提供者Api类型</param>
        public ExternalLoginProviderInfo(string name, string clientId, string clientSecret, Type providerApiType)
        {
            Name = name;
            ClientId = clientId;
            ClientSecret = clientSecret;
            ProviderApiType = providerApiType;
        }
    }
}
