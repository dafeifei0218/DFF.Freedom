using DFF.Freedom.EntityFramework;
using EntityFramework.DynamicFilters;

namespace DFF.Freedom.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly FreedomDbContext _context;

        public InitialHostDbBuilder(FreedomDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
