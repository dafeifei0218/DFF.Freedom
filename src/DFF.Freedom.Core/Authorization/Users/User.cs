using System;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 用户实体，继承自AbpUser。
    /// </summary>
    public class User : AbpUser<User>
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public const string DefaultPassword = "123qwe";

        /// <summary>
        /// 创建随机密码
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        /// <summary>
        /// 创建租户的管理员用户
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="emailAddress">Email地址</param>
        /// <returns></returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}