﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.MultiTenancy;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Authorization.Users
{
    /// <summary>
    /// 用户注册管理类
    /// </summary>
    public class UserRegistrationManager : DomainService
    {
        public IAbpSession AbpSession { get; set; }

        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantManager">租户管理</param>
        /// <param name="userManager">用户管理</param>
        /// <param name="roleManager">角色管理</param>
        public UserRegistrationManager(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 注册 异步方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="surname">真实名称</param>
        /// <param name="emailAddress">邮件地址</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="plainPassword">普通的密码</param>
        /// <param name="isEmailConfirmed">是否邮件确认</param>
        /// <returns></returns>
        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed)
        {
            CheckForTenant();

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                Password = _userManager.PasswordHasher.HashPassword(plainPassword),
                IsEmailConfirmed = true,
                Roles = new List<UserRole>()
            };

            foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
            {
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("Can not register host users!");
            }
        }

        /// <summary>
        /// 获取激活的租户 异步方法
        /// </summary>
        /// <returns></returns>
        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        /// <summary>
        /// 获取激活的租户 异步方法
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult">认证结果</param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
