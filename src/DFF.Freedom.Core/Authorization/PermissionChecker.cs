using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Users;

namespace DFF.Freedom.Authorization
{
    /// <summary>
    /// 权限检查器
    /// </summary>
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManager">用户管理</param>
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
