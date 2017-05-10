using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Editions;
using DFF.Freedom.MultiTenancy.Dto;
using DFF.Freedom.Users;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户 应用程序服务
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : FreedomAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly EditionManager _editionManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantManager">租户管理</param>
        /// <param name="roleManager">角色管理</param>
        /// <param name="editionManager">版本管理</param>
        /// <param name="abpZeroDbMigrator">AbpZero数据迁移</param>
        public TenantAppService(
            TenantManager tenantManager, 
            RoleManager roleManager, 
            EditionManager editionManager, 
            IAbpZeroDbMigrator abpZeroDbMigrator)
        {
            _tenantManager = tenantManager;
            _roleManager = roleManager;
            _editionManager = editionManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }

        /// <summary>
        /// 获取租户列表
        /// </summary>
        /// <returns>租户列表</returns>
        public ListResultDto<TenantListDto> GetTenants()
        {
            return new ListResultDto<TenantListDto>(
                _tenantManager.Tenants
                    .OrderBy(t => t.TenancyName)
                    .ToList()
                    .MapTo<List<TenantListDto>>()
                );
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        public async Task CreateTenant(CreateTenantInput input)
        {
            //Create tenant
            //创建租户
            var tenant = input.MapTo<Tenant>();
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            //CheckErrors(await TenantManager.CreateAsync(tenant));
            await TenantManager.CreateAsync(tenant); //2017-05-10：新加的语句
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id. 获取新租户的Id

            //Create tenant database
            //创建租户数据库
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            //我们是新租户的工作实体，所以更换过滤器
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                //为新租户创建静态角色
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids 获取静态角色的Id

                //grant all permissions to admin role
                //授予管理员角色的所有权限
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                //为租户创建管理员用户
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckErrors(await UserManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id 获取管理员用户的Id

                //Assign admin user to role!
                //将管理员用户分配给角色
                CheckErrors(await UserManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}