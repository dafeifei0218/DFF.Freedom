using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using DFF.Freedom.EntityFrameworkCore.Seed;

namespace DFF.Freedom.EntityFrameworkCore
{
    [DependsOn(
        typeof(FreedomCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class FreedomEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }


        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<FreedomDbContext>(configuration =>
                {
                    FreedomDbContextConfigurer.Configure(configuration.DbContextOptions, configuration.ConnectionString);
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FreedomEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}