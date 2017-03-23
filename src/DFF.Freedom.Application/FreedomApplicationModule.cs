using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using DFF.Freedom.Authorization;

namespace DFF.Freedom
{
    /// <summary>
    /// 应用程序模块
    /// </summary>
    [DependsOn(
        typeof(FreedomCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FreedomApplicationModule : AbpModule
    {
        /// <summary>
        /// 初始化之前执行的方法
        /// </summary>
        public override void PreInitialize()
        {
            //授权设置
            Configuration.Authorization.Providers.Add<FreedomAuthorizationProvider>();
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public override void Initialize()
        {
            //注册程序集
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}