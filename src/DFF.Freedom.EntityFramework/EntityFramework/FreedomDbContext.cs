using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Microsoft.Extensions.Configuration;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Configuration;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Users;
using DFF.Freedom.Web;

namespace DFF.Freedom.EntityFramework
{
    [DbConfigurationType(typeof(FreedomDbConfiguration))]
    public class FreedomDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        /* Default constructor is needed for EF command line tool. */
        public FreedomDbContext()
            : base(GetConnectionString())
        {

        }

        private static string GetConnectionString()
        {
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder()
                );

            return configuration.GetConnectionString(
                FreedomConsts.ConnectionStringName
                );
        }

        /* This constructor is used by ABP to pass connection string.
         * Notice that, actually you will not directly create an instance of FreedomDbContext since ABP automatically handles it.
         */
        public FreedomDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection. */
        public FreedomDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }

        public FreedomDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }

    public class FreedomDbConfiguration : DbConfiguration
    {
        public FreedomDbConfiguration()
        {
            SetProviderServices(
                "System.Data.SqlClient",
                System.Data.Entity.SqlServer.SqlProviderServices.Instance
            );
        }
    }
}
