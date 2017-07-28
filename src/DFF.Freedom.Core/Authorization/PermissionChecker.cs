using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
