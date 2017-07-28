using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace DFF.Freedom.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<IdentityOptions> options, 
            SignInManager signInManager) 
            : base(options, signInManager)
        {
        }
    }
}