using System.Collections.Generic;
using System.Linq;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Web.Models.Users
{
    /// <summary>
    /// 编辑用户模态框 视图模型
    /// </summary>
    public class EditUserModalViewModel
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public IReadOnlyList<RoleDto> Roles { get; set; }

        /// <summary>
        /// 用户是否属于给定的角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public bool UserIsInRole(RoleDto role)
        {
            return User.Roles != null && User.Roles.Any(r => r == role.DisplayName);
        }
    }
}