using System.Threading.Tasks;
using Abp.Dependency;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证提供者Api基类
    /// </summary>
    public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
    {
        /// <summary>
        /// 提供者信息
        /// </summary>
        public ExternalLoginProviderInfo ProviderInfo { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="providerInfo">外部登录提供者信息</param>
        public void Initialize(ExternalLoginProviderInfo providerInfo)
        {
            ProviderInfo = providerInfo;
        }

        /// <summary>
        /// 是否有效用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        public async Task<bool> IsValidUser(string userId, string accessCode)
        {
            var userInfo = await GetUserInfo(accessCode);
            return userInfo.ProviderKey == userId;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
    }
}
