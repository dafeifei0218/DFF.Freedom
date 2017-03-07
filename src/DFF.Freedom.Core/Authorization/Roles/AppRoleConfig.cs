using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// Abp角色配置
    /// </summary>
    public static class AppRoleConfig
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="roleManagementConfig">角色管理配置</param>
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            //静态租主角色
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
                );

            //Static tenant roles

            //静态租户角色
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant)
                );
        }
    }
}
