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
            //Common permissions
            //公共权限
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            //租户权限
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
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
