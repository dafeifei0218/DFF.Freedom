using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Roles
{
    /// <summary>
    /// 角色 应用程序服务接口
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="input">更新角色权限输入模型</param>
        /// <returns></returns>
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
