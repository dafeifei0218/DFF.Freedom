using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using DFF.Freedom.Authorization.Roles;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UserManager : AbpUserManager<Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userStore">用户存储</param>
        /// <param name="roleManager">角色管理</param>
        /// <param name="permissionManager">权限管理</param>
        /// <param name="unitOfWorkManager">工作单元</param>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="organizationUnitRepository">组织单元仓储</param>
        /// <param name="userOrganizationUnitRepository">用户组织单元仓储</param>
        /// <param name="organizationUnitSettings">组织单元设置</param>
        /// <param name="localizationManager">本地化管理</param>
        /// <param name="settingManager">设置管理</param>
        /// <param name="emailService">Email服务</param>
        /// <param name="userTokenProviderAccessor">用户令牌提供者访问器</param>
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                  userStore,
                  roleManager,
                  permissionManager,
                  unitOfWorkManager,
                  cacheManager,
                  organizationUnitRepository,
                  userOrganizationUnitRepository,
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor)
        {
        }
    }
}