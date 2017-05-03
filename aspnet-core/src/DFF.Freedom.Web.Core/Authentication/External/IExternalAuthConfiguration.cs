using System.Collections.Generic;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证配置
    /// </summary>
    public interface IExternalAuthConfiguration
    {
        /// <summary>
        /// 外部登录提供者信息列表
        /// </summary>
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
