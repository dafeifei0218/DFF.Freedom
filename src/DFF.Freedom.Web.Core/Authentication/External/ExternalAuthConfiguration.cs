using System.Collections.Generic;
using Abp.Dependency;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证配置
    /// </summary>
    public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
    {
        /// <summary>
        /// 外部登录信息列表的提供者
        /// </summary>
        public List<ExternalLoginProviderInfo> Providers { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExternalAuthConfiguration()
        {
            Providers = new List<ExternalLoginProviderInfo>();
        }
    }
}
