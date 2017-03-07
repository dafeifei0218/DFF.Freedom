using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using DFF.Freedom.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// ��ɫ�洢
    /// </summary>
    public class RoleStore : AbpRoleStore<Role, User>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="roleRepository">��ɫ�ִ�</param>
        /// <param name="userRoleRepository">�û���ɫ�ִ�</param>
        /// <param name="rolePermissionSettingRepository">��ɫȨ�����òִ�</param>
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}