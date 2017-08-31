using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证管理类
    /// </summary>
    public class ExternalAuthManager : IExternalAuthManager, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="externalAuthConfiguration"></param>
        public ExternalAuthManager(IIocResolver iocResolver, IExternalAuthConfiguration externalAuthConfiguration)
        {
            _iocResolver = iocResolver;
            _externalAuthConfiguration = externalAuthConfiguration;
        }

        /// <summary>
        /// 是否有效的用户
        /// </summary>
        /// <param name="provider">提供者字符串</param>
        /// <param name="providerKey">提供者键</param>
        /// <param name="providerAccessCode">提供者访问代码</param>
        /// <returns></returns>
        public Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode)
        {
            using (var providerApi = CreateProviderApi(provider))
            {
                return providerApi.Object.IsValidUser(providerKey, providerAccessCode);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="provider">提供者字符串</param>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        public Task<ExternalAuthUserInfo> GetUserInfo(string provider, string accessCode)
        {
            using (var providerApi = CreateProviderApi(provider))
            {
                return providerApi.Object.GetUserInfo(accessCode);
            }
        }

        public IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> CreateProviderApi(string provider)
        {
            var providerInfo = _externalAuthConfiguration.Providers.FirstOrDefault(p => p.Name == provider);
            if (providerInfo == null)
            {
                throw new Exception("Unknown external auth provider: " + provider);
            }

            var providerApi = _iocResolver.ResolveAsDisposable<IExternalAuthProviderApi>(providerInfo.ProviderApiType);
            providerApi.Object.Initialize(providerInfo);
            return providerApi;
        }
    }
}
