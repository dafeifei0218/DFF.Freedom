using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace DFF.Freedom.Localization
{
    /// <summary>
    /// 本地化配置者
    /// </summary>
    public static class FreedomLocalizationConfigurer
    {
        /// <summary>
        /// 配置本地化资源的方法
        /// </summary>
        /// <param name="localizationConfiguration">本地化配置接口</param>
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(FreedomConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FreedomLocalizationConfigurer).GetAssembly(),
                        "DFF.Freedom.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}