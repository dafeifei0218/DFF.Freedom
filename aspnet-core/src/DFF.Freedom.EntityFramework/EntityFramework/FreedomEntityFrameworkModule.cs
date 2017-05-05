using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace DFF.Freedom.EntityFramework
{
    /// <summary>
    /// EntityFramework模块
    /// </summary>
    [DependsOn(
        typeof(FreedomCoreModule), 
        typeof(AbpZeroEntityFrameworkModule))]
    public class FreedomEntityFrameworkModule : AbpModule
    {
        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<FreedomDbContext>());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}