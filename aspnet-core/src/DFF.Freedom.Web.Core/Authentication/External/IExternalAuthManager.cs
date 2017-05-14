using System.Threading.Tasks;
using Abp.Zero.AspNetCore;

namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证管理接口
    /// </summary>
    public interface IExternalAuthManager
    {
        /// <summary>
        /// 是否有效的用户
        /// </summary>
        /// <param name="provider">提供者字符串</param>
        /// <param name="providerKey">提供者键</param>
        /// <param name="providerAccessCode">提供者访问代码</param>
        /// <returns></returns>
        Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode);
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="provider">提供者字符串</param>
        /// <param name="accessCode">访问代码</param>
        /// <returns></returns>
        Task<ExternalLoginUserInfo> GetUserInfo(string provider, string accessCode);
    }
}
