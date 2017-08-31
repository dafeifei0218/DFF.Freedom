using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Sessions
{
    /// <summary>
    /// Session会话 应用程序服务接口
    /// </summary>
    public interface ISessionAppService : IApplicationService
    {
        /// <summary>
        /// 获取当前登录信息
        /// </summary>
        /// <returns></returns>
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
