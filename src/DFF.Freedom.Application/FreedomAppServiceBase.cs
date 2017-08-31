using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using DFF.Freedom.MultiTenancy;
using Abp.Runtime.Session;
using Abp.IdentityFramework;
using DFF.Freedom.Authorization.Users;
using Microsoft.AspNetCore.Identity;

namespace DFF.Freedom
{
    /// <summary>
    /// Derive your application services from this class.
    /// 应用程序服务基类
    /// </summary>
    public abstract class FreedomAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 租户管理
        /// </summary>
        public TenantManager TenantManager { get; set; }

        /// <summary>
        /// 用户管理
        /// </summary>
        public UserManager UserManager { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected FreedomAppServiceBase()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 获取当前用户 异步方法
        /// </summary>
        /// <returns>用户信息</returns>
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        /// <summary>
        /// 获取当前租户 异步方法
        /// </summary>
        /// <returns>返回 <see cref="Tenant"/> 租户信息</returns>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
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