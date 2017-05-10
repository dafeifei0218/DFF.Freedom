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
    /// �⻧ Ӧ�ó������
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : FreedomAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly RoleManager _roleManager;
        private readonly EditionManager _editionManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tenantManager">�⻧����</param>
        /// <param name="roleManager">��ɫ����</param>
        /// <param name="editionManager">�汾����</param>
        /// <param name="abpZeroDbMigrator">AbpZero����Ǩ��</param>
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
        /// ��ȡ�⻧�б�
        /// </summary>
        /// <returns>�⻧�б�</returns>
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
        /// �����⻧
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        public async Task CreateTenant(CreateTenantInput input)
        {
            //Create tenant
            //�����⻧
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
            await TenantManager.CreateAsync(tenant); //2017-05-10���¼ӵ����
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id. ��ȡ���⻧��Id

            //Create tenant database
            //�����⻧���ݿ�
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            //���������⻧�Ĺ���ʵ�壬���Ը���������
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                //Ϊ���⻧������̬��ɫ
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids ��ȡ��̬��ɫ��Id

                //grant all permissions to admin role
                //�������Ա��ɫ������Ȩ��
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                //Ϊ�⻧��������Ա�û�
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckErrors(await UserManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id ��ȡ����Ա�û���Id

                //Assign admin user to role!
                //������Ա�û��������ɫ
                CheckErrors(await UserManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }
    }
}