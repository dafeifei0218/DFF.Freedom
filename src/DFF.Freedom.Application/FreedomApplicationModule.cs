using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using DFF.Freedom.Authorization;

namespace DFF.Freedom
{
    [DependsOn(
        typeof(FreedomCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FreedomApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<FreedomAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}