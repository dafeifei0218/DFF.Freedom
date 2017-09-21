using System.Collections.Generic;
using Abp.Configuration;

namespace DFF.Freedom.Configuration
{
    /// <summary>
    /// 应用程序配置提供者
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        /// <summary>
        /// 获取配置定义
        /// </summary>
        /// <param name="context">配置定义提供者上下文</param>
        /// <returns></returns>
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
            };
        }
    }
}
