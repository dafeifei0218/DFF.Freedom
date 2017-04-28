using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Authorization.Roles
{
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public class RoleManager : AbpRoleManager<Role, User>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="store">��ɫ�洢</param>
        /// <param name="permissionManager">Ȩ�޹���</param>
        /// <param name="roleManagementConfig">��ɫ��������</param>
        /// <param name="cacheManager">�������</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
        }
    }
}