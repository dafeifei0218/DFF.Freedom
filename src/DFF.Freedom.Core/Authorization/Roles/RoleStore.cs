using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using DFF.Freedom.Authorization.Users;

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
        /// <param name="unitOfWorkManager">工作单元管理类</param>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="rolePermissionSettingRepository">角色权限设置仓储</param>
        public RoleStore(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Role> roleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                unitOfWorkManager,
                roleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}