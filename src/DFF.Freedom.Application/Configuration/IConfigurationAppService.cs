using System.Threading.Tasks;
using DFF.Freedom.Configuration.Dto;

namespace DFF.Freedom.Configuration
{
    /// <summary>
    /// 配置 应用程序服务类接口
    /// </summary>
    public interface IConfigurationAppService
    {
        /// <summary>
        /// 改变UI主题
        /// </summary>
        /// <param name="input">改变UI主题 输入模型</param>
        /// <returns></returns>
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}