using DFF.Freedom.Configuration;
using DFF.Freedom.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DFF.Freedom.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FreedomDbContextFactory : IDbContextFactory<FreedomDbContext>
    {
        public FreedomDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<FreedomDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            FreedomDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FreedomConsts.ConnectionStringName));
            
            return new FreedomDbContext(builder.Options);
        }
    }
}