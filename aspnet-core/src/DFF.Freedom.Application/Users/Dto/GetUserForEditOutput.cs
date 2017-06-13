using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// 获取编辑页面的用户信息 输出模型
    /// </summary>
    public class GetUserForEditOutput
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Role> Roles { get; set; }

    }
}
