using System.Collections.Generic;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using DFF.Freedom.Authorization.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

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
        /// <param name="roleValidators"></param>
        /// <param name="keyNormalizer"></param>
        /// <param name="errors"></param>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="permissionManager">权限管理</param>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        /// <param name="roleManagementConfig">角色管理配置</param>
        public RoleManager(
            RoleStore store, 
            IEnumerable<IRoleValidator<Role>> roleValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            ILogger<AbpRoleManager<Role, User>> logger,
            IHttpContextAccessor contextAccessor, 
            IPermissionManager permissionManager, 
            ICacheManager cacheManager, 
            IUnitOfWorkManager unitOfWorkManager,
            IRoleManagementConfig roleManagementConfig)
            : base(
                  store,
                  roleValidators, 
                  keyNormalizer, 
                  errors, logger, 
                  contextAccessor, 
                  permissionManager,
                  cacheManager, 
                  unitOfWorkManager,
                  roleManagementConfig)
        {
        }
    }
}