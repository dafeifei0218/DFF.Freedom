using System;
using System.Collections.Generic;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using DFF.Freedom.Authorization.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UserManager : AbpUserManager<Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleManager">角色管理</param>
        /// <param name="store">用户昂出</param>
        /// <param name="optionsAccessor"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="userValidators"></param>
        /// <param name="passwordValidators"></param>
        /// <param name="keyNormalizer"></param>
        /// <param name="errors"></param>
        /// <param name="services">服务提供者</param>
        /// <param name="logger">日志</param>
        /// <param name="permissionManager">权限管理</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="organizationUnitRepository">组织机构单元仓储</param>
        /// <param name="userOrganizationUnitRepository">用户组织机构单元仓储</param>
        /// <param name="organizationUnitSettings">组织机构单元设置</param>
        /// <param name="settingManager">设置管理</param>
        public UserManager(
            RoleManager roleManager,
            UserStore store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger, 
            IPermissionManager permissionManager, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager, 
            IRepository<OrganizationUnit, long> organizationUnitRepository, 
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, 
            IOrganizationUnitSettings organizationUnitSettings, 
            ISettingManager settingManager)
            : base(
                roleManager, 
                store, 
                optionsAccessor, 
                passwordHasher, 
                userValidators, 
                passwordValidators, 
                keyNormalizer, 
                errors, 
                services, 
                logger, 
                permissionManager, 
                unitOfWorkManager, 
                cacheManager,
                organizationUnitRepository, 
                userOrganizationUnitRepository, 
                organizationUnitSettings, 
                settingManager)
        {
        }
    }
}