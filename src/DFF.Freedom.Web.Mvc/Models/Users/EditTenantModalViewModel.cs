using System.Collections.Generic;
using System.Linq;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Web.Models.Users
{
    /// <summary>
    /// �༭�û�ģ̬�� ��ͼģ��
    /// </summary>
    public class EditUserModalViewModel
    {
        /// <summary>
        /// �û�
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// ��ɫ�б�
        /// </summary>
        public IReadOnlyList<RoleDto> Roles { get; set; }

        /// <summary>
        /// �û��Ƿ����ڸ����Ľ�ɫ
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <returns></returns>
        public bool UserIsInRole(RoleDto role)
        {
            return User.Roles != null && User.Roles.Any(r => r == role.DisplayName);
        }
    }
}