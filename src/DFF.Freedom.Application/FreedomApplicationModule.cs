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
        public override void PreInitialize()
        {
            //授权设置
            Configuration.Authorization.Providers.Add<FreedomAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}