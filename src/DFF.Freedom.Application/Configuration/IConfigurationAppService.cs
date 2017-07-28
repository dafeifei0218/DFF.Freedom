using System.Threading.Tasks;
using DFF.Freedom.Configuration.Dto;

namespace DFF.Freedom.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}