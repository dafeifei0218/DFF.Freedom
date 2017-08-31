using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using DFF.Freedom.Localization;
using Abp.Zero.Configuration;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Configuration;
using DFF.Freedom.Timing;

namespace DFF.Freedom
{
    /// <summary>
    /// 核心模块
    /// </summary>
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class FreedomCoreModule : AbpModule
    {
        /// <summary>
        /// 初始化之前执行
        /// </summary>
        public override void PreInitialize()
        {
            //是否启用匿名用户
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            //声明实体类型
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //配置本地化
            FreedomLocalizationConfigurer.Configure(Configuration.Localization);

            //Enable this line to create a multi-tenant application.
            //启用多租户应用程序
            Configuration.MultiTenancy.IsEnabled = FreedomConsts.MultiTenancyEnabled;

            //Configure roles
            //配置角色管理
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        /// <summary>
        /// 初始化执行的方法
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FreedomCoreModule).GetAssembly());
        }

        /// <summary>
        /// 初始化之后执行的方法
        /// </summary>
        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}