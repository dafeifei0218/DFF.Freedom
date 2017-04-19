using Abp.Extensions;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Web.Authentication
{
    /// <summary>
    /// 外部登录用户信息
    /// </summary>
    public class ExternalLoginUserInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 真是名
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 登录信息
        /// </summary>
        public UserLoginInfo LoginInfo { get; set; }

        /// <summary>
        /// 是否全部为空
        /// </summary>
        /// <returns>true：全部为空；</returns>
        public bool HasAllNonEmpty()
        {
            return !Name.IsNullOrEmpty() &&
                   !Surname.IsNullOrEmpty() &&
                   !EmailAddress.IsNullOrEmpty();
        }
    }
}