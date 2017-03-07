using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using DFF.Freedom.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// 角色存储
    /// </summary>
    public class RoleStore : AbpRoleStore<Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="userRoleRepository">用户角色仓储</param>
        /// <param name="rolePermissionSettingRepository">角色权限设置仓储</param>
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}