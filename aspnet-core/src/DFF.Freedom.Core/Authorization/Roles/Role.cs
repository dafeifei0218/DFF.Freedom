using Abp.Authorization.Roles;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// 角色 实体
    /// </summary>
    public class Role : AbpRole<User>
    {
        //Can add application specific role properties here
        //可以在这里添加特定于应用程序的角色属性

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
    }
}