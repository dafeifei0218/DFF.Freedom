using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using DFF.Freedom.Authorization.Roles;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 用户存储类
    /// </summary>
    public class UserStore : AbpUserStore<Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="userLoginRepository">用户登录仓储</param>
        /// <param name="userRoleRepository">用户角色仓储</param>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="userPermissionSettingRepository">用户权限设置仓储</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="userClaimRepository">用户请求仓储</param>
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimRepository)
        {
        }
    }
}