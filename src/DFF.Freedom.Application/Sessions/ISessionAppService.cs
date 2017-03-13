using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Sessions
{
    /// <summary>
    /// Session服务
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
