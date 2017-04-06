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
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "fa fa-info"
                        )
                );



        }

        /// <summary>
        /// 根据名称，获取本地化值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FreedomConsts.LocalizationSourceName);
        }
    }
}
