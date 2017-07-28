using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
