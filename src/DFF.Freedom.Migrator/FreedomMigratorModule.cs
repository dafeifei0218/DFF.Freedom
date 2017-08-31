using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using DFF.Freedom.Configuration;
using DFF.Freedom.EntityFrameworkCore;
using DFF.Freedom.Migrator.DependencyInjection;

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
		/// <param></param>
        public FreedomMigratorModule(FreedomEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(FreedomMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
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
            IocManager.RegisterAssemblyByConvention(typeof(FreedomMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}