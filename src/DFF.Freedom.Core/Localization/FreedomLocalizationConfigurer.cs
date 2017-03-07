using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;

namespace DFF.Freedom.Localization
{
    /// <summary>
    /// 本地化配置者
    /// </summary>
    public static class FreedomLocalizationConfigurer
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="localizationConfiguration">本地化配置接口</param>
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(FreedomConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "DFF.Freedom.Core.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}