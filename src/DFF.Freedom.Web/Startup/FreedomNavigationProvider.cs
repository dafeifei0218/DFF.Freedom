using Abp.Application.Navigation;
using Abp.Localization;
using DFF.Freedom.Authorization;

namespace DFF.Freedom.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// 这个类定义应用程序的导航。
    /// </summary>
    public class FreedomNavigationProvider : NavigationProvider
    {
        /// <summary>
        /// 设置导航
        /// </summary>
        /// <param name="context">导航提供者上下文</param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Dashboard,
                        L("Dashboard"),
                        url: "Dashboard",
                        icon: "icon-home"//, requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Manage,
                        L("Manage"),
                        url: "Manage",
                        icon: "icon-wrench" //, requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "OrganizationUnits",
                            icon: "icon-layers"//, requiredPermissionName: PermissionNames.Pages_OrganizationUnits
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Roles,
                            L("Roles"),
                            url: "Roles",
                            icon: "icon-briefcase"//, requiredPermissionName: PermissionNames.Pages_Roles
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Users,
                            L("Users"),
                            url: "Users",
                            icon: "icon-users",
                            requiredPermissionName: PermissionNames.Pages_Users
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Languages,
                            L("Languages"),
                            url: "Languages",
                            icon: "icon-flag"//, requiredPermissionName: PermissionNames.Pages_Roles
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.AuditLogs,
                            L("AuditLogs"),
                            url: "AuditLogs",
                            icon: "icon-lock"//, requiredPermissionName: PermissionNames.Pages_AuditLogs
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Settings,
                            L("Settings"),
                            url: "Settings",
                            icon: "icon-settings"//, requiredPermissionName: PermissionNames.Pages_Settings
                            )
                    ) 
                )
                //.AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Tenants,
                //        L("Tenants"),
                //        url: "Tenants",
                //        icon: "icon-inbox" //, requiredPermissionName: PermissionNames.Pages_Tenants
                //        )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.About,
                //        L("About"),
                //        url: "About",
                //        icon: "icon-info-sign"
                //        )
                //)
                ;
        }

        /// <summary>
        /// 根据键名称，获取本地化值
        /// </summary>
        /// <param name="name">键名称</param>
        /// <returns></returns>
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FreedomConsts.LocalizationSourceName);
        }
    }
}
