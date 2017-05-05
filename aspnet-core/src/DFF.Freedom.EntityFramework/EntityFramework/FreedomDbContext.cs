using System.Data.Common;
using System.Data.Entity;
using Abp.Notifications;
using Abp.Zero.EntityFramework;
using Microsoft.Extensions.Configuration;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Configuration;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Web;

namespace DFF.Freedom.EntityFramework
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [DbConfigurationType(typeof(FreedomDbConfiguration))]
    public class FreedomDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        /* Default constructor is needed for EF command line tool. */
        /// <summary>
        /// 构造函数
        /// </summary>
        public FreedomDbContext()
            : base(GetConnectionString())
        {

        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nameOrConnectionString">名称或连接字符串</param>
        public FreedomDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection. */
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        public FreedomDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="contextOwnsConnection"></param>
        public FreedomDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class FreedomDbConfiguration : DbConfiguration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FreedomDbConfiguration()
        {
            SetProviderServices(
                "System.Data.SqlClient",
                System.Data.Entity.SqlServer.SqlProviderServices.Instance
            );
        }
    }
}
