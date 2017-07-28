using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Authorization.Accounts.Dto;

namespace DFF.Freedom.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
