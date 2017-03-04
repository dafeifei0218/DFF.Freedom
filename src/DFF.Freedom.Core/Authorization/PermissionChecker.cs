using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Users;

namespace DFF.Freedom.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
