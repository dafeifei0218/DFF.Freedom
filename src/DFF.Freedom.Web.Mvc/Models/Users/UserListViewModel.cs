using System.Collections.Generic;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}