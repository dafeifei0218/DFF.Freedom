using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;

namespace DFF.Freedom.EntityFramework
{
    /// <summary>
    /// AbpZero数据库迁移
    /// </summary>
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<FreedomDbContext, Migrations.Configuration>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="connectionStringResolver"></param>
        /// <param name="iocResolver"></param>
        public AbpZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
