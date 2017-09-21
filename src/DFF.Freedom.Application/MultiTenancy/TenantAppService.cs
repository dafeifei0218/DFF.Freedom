using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Editions;
using DFF.Freedom.MultiTenancy.Dto;
using Microsoft.AspNetCore.Identity;
using Abp.IdentityFramework;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户 应用程序服务
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IPasswordHasher<User> _passwordHasher;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="tenantManager">租户管理</param>
        /// <param name="editionManager">版本管理</param>
        /// <param name="userManager"></param>
        /// <param name="roleManager">角色管理</param>
        /// <param name="abpZeroDbMigrator">AbpZero数据迁移</param>
        /// <param name="passwordHasher"></param>
        public TenantAppService(
            IRepository<Tenant, int> repository, 
            TenantManager tenantManager, 
            EditionManager editionManager,
            UserManager userManager,            
            RoleManager roleManager, 
            IAbpZeroDbMigrator abpZeroDbMigrator, 
            IPasswordHasher<User> passwordHasher
            ) : base(repository)
        {
            _tenantManager = tenantManager; 
            _editionManager = editionManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        public override async Task<TenantDto> Create(CreateTenantDto input)
        {
            CheckCreatePermission();

            //Create tenant
			//创建租户
            var tenant = ObjectMapper.Map<Tenant>(input);
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

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
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress);
                adminUser.Password = _passwordHasher.HashPassword(adminUser, User.DefaultPassword);
                CheckErrors(await _userManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                //Assign admin user to role!
				//将管理员用户分配给角色
                CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="updateInput">更新租户 输入模型</param>
        /// <param name="entity">租户实体</param>
        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            //Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input">实体 输入模型</param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult">认证结果</param>
        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}