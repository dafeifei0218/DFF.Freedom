using System.Linq;
using DFF.Freedom.EntityFramework;
using DFF.Freedom.MultiTenancy;

namespace DFF.Freedom.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly FreedomDbContext _context;

        public DefaultTenantCreator(FreedomDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
