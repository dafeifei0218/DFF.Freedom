using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using DFF.Freedom.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManager : AbpRoleManager<Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="store">角色存储</param>
        /// <param name="permissionManager">权限管理</param>
        /// <param name="roleManagementConfig">角色管理配置</param>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
        }
    }
}