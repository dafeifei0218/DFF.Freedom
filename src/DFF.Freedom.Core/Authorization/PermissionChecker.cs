using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization
{
    /// <summary>
    /// 权限检查器
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
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
