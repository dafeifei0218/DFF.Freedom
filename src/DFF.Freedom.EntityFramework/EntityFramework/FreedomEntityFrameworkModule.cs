using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace DFF.Freedom.EntityFramework
{
    [DependsOn(
        typeof(FreedomCoreModule), 
        typeof(AbpZeroEntityFrameworkModule))]
    public class FreedomEntityFrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<FreedomDbContext>());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}