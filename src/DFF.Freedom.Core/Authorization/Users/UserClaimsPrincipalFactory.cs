using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="optionsAccessor"></param>
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
