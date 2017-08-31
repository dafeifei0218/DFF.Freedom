using System.Threading.Tasks;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证提供者Api接口
    /// </summary>
    public interface IExternalAuthProviderApi
    {
        /// <summary>
        /// 外部登录提供者信息
        /// </summary>
        ExternalLoginProviderInfo ProviderInfo { get; }

        /// <summary>
        /// 是否有效用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        Task<bool> IsValidUser(string userId, string accessCode);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="providerInfo">外部登录提供者信息</param>
        void Initialize(ExternalLoginProviderInfo providerInfo);
    }
}
