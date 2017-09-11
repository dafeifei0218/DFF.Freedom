using System.Collections.Generic;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Web.Models.Users
{
    /// <summary>
    /// �û��б� ��ͼģ��
    /// </summary>
    public class UserListViewModel
    {
        /// <summary>
        /// �û��б�
        /// </summary>
        public IReadOnlyList<UserDto> Users { get; set; }

        /// <summary>
        /// ��ɫ�б�
        /// </summary>
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}