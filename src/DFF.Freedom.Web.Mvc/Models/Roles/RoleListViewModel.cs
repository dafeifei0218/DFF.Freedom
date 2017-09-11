using System.Collections.Generic;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Web.Models.Roles
{
    /// <summary>
    /// 角色列表 视图模型
    /// </summary>
    public class RoleListViewModel
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public IReadOnlyList<RoleDto> Roles { get; set; }

        /// <summary>
        /// 权限是列表
        /// </summary>
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
