using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DFF.Freedom.Configuration.Dto;

namespace DFF.Freedom.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : FreedomAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
