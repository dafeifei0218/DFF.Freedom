using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace DFF.Freedom.EntityFramework.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public abstract class FreedomRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FreedomDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider">数据库上下文提供者</param>
        protected FreedomRepositoryBase(IDbContextProvider<FreedomDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
        //为所有仓储，添加通用方法
    }

    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public abstract class FreedomRepositoryBase<TEntity> : FreedomRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider">数据库上下文提供者</param>
        protected FreedomRepositoryBase(IDbContextProvider<FreedomDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
        //不要在这里添加任何方法，添加到上面的类（因为继承了它）
    }
}
