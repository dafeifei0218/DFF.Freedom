using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// 角色 实体
    /// </summary>
    public class Role : AbpRole<User>
    {
        public const int MaxDescriptionLength = 5000;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="displayName">角色显示名称</param>
        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="name">角色名</param>
        /// <param name="displayName">角色显示名称</param>
        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }

        [MaxLength(MaxDescriptionLength)]
        public string Description {get; set;}
    }
}