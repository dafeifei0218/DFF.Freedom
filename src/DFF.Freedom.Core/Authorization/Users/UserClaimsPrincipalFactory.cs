using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 用户要求主要工厂
    /// </summary>
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManager">用户管理</param>
        /// <param name="roleManager">角色管理</param>
        /// <param name="optionsAccessor">选项访问器</param>
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }
    }
}
