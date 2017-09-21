using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DFF.Freedom.Configuration.Dto;

namespace DFF.Freedom.Configuration
{
    /// <summary>
    /// 配置 应用程序服务类
    /// </summary>
    [AbpAuthorize]
    public class ConfigurationAppService : FreedomAppServiceBase, IConfigurationAppService
    {
        /// <summary>
        /// 改变UI主题
        /// </summary>
        /// <param name="input">改变UI主题 输入模型</param>
        /// <returns></returns>
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
