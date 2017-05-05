using System.Data.Entity;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using DFF.Freedom.Configuration;
using DFF.Freedom.EntityFramework;

namespace DFF.Freedom.Migrator
{
    /// <summary>
    /// 迁移模块
    /// </summary>
    [DependsOn(typeof(FreedomEntityFrameworkModule))]
    public class FreedomMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FreedomMigratorModule()
        {
            _appConfiguration = AppConfigurations.Get(
                typeof(FreedomMigratorModule).Assembly.GetDirectoryPathOrNull()
            );
        }

        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
            Database.SetInitializer<FreedomDbContext>(null);

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                FreedomConsts.ConnectionStringName
                );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        /// <summary>
        /// 初始化执行的方法
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}