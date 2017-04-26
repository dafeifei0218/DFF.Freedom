using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
