using Abp.Zero.EntityFrameworkCore;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace DFF.Freedom.EntityFrameworkCore
{
    public class FreedomDbContext : AbpZeroDbContext<Tenant, Role, User, FreedomDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        
        public FreedomDbContext(DbContextOptions<FreedomDbContext> options)
            : base(options)
        {

        }
    }
}
