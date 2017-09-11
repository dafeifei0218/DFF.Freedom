using System.Collections.Generic;
using System.Linq;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Web.Models.Roles
{
    /// <summary>
    /// 编辑角色 视图模型
    /// </summary>
    public class EditRoleModalViewModel
    {
        /// <summary>
        /// 角色
        /// </summary>
        public RoleDto Role { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public IReadOnlyList<PermissionDto> Permissions { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public bool HasPermission(PermissionDto permission)
        {
            return Permissions != null && Role.Permissions.Any(p => p == permission.Name);
        }
    }
}
