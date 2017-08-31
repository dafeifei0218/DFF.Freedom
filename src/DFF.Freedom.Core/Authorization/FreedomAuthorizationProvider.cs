using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace DFF.Freedom.Authorization
{
    /// <summary>
    /// 授权提供者
    /// </summary>
    public class FreedomAuthorizationProvider : AuthorizationProvider
    {
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="context">权限定义上下文</param>
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        /// <summary>
        /// 获取本地化名称，对应的值
        /// </summary>
        /// <param name="name">本地化名称</param>
        /// <returns></returns>
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FreedomConsts.LocalizationSourceName);
        }
    }
}
