using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq;
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
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="roleRepository">角色仓储</param>
		
        /// <param name="asyncQueryableExecuter"></param>
        /// <param name="userRoleRepository">用户角色仓储</param>
		
		
        /// <param name="userLoginRepository">用户登录仓储</param>
        /// <param name="userClaimRepository">用户请求仓储</param>
        /// <param name="userPermissionSettingRepository">用户权限设置仓储</param>
        public UserStore(
            IUnitOfWorkManager unitOfWorkManager, 
            IRepository<User, long> userRepository, 
            IRepository<Role> roleRepository, 
            IAsyncQueryableExecuter asyncQueryableExecuter, 
            IRepository<UserRole, long> userRoleRepository, 
            IRepository<UserLogin, long> userLoginRepository, 
            IRepository<UserClaim, long> userClaimRepository, 
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository) 
            : base(
                  unitOfWorkManager, 
                  userRepository, 
                  roleRepository, 
                  asyncQueryableExecuter, 
                  userRoleRepository, 
                  userLoginRepository, 
                  userClaimRepository,
                  userPermissionSettingRepository)
        {
        }
    }
}