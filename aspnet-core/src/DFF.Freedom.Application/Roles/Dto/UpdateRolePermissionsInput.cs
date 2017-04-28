using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Roles.Dto
{
    /// <summary>
    /// 更新角色权限输入模型
    /// </summary>
    public class UpdateRolePermissionsInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

        /// <summary>
        /// 授予权限名称
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}