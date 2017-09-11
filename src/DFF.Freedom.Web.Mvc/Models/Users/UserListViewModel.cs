using System.Collections.Generic;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Web.Models.Users
{
    /// <summary>
    /// 用户列表 视图模型
    /// </summary>
    public class UserListViewModel
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public IReadOnlyList<UserDto> Users { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}