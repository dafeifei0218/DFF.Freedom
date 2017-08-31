﻿using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.MultiTenancy;
using Microsoft.AspNetCore.Identity;

namespace DFF.Freedom.Authorization
{
    /// <summary>
    /// 登录管理类
    /// </summary>
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userManager">用户管理</param>
        /// <param name="multiTenancyConfig">多租户配置</param>
        /// <param name="tenantRepository">租主仓储</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="settingManager">设置管理</param>
        /// <param name="userLoginAttemptRepository">用户登录尝试仓储</param>
        /// <param name="userManagementConfig">用户管理配置</param>
        /// <param name="iocResolver">IOC控制反转解析器</param>
		/// <param></param>		
        /// <param name="roleManager">角色管理</param>
		/// <param></param>		
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            IPasswordHasher<User> passwordHasher, 
            RoleManager roleManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory) 
            : base(
                  userManager, 
                  multiTenancyConfig,
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  passwordHasher, 
                  roleManager, 
                  claimsPrincipalFactory)
        {
        }
    }
}
