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
    /// 
    /// </summary>
    public class GetUserForEditOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Role> Roles { get; set; }

    }
}
