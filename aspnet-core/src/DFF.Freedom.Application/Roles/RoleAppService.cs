using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Roles
{
    /* THIS IS JUST A SAMPLE. */
    //这只是一个样本
    /// <summary>
    /// 
    /// </summary>
    public class RoleAppService : FreedomAppServiceBase,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleManager">角色管理</param>
        /// <param name="permissionManager">权限管理</param>
        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="input">更新角色权限输入模型</param>
        /// <returns></returns>
        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
    }
}