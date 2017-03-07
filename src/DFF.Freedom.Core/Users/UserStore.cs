using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using DFF.Freedom.Authorization.Roles;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// �û��洢��
    /// </summary>
    public class UserStore : AbpUserStore<Role, User>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userRepository">�û��ִ�</param>
        /// <param name="userLoginRepository">�û���¼�ִ�</param>
        /// <param name="userRoleRepository">�û���ɫ�ִ�</param>
        /// <param name="roleRepository">��ɫ�ִ�</param>
        /// <param name="userPermissionSettingRepository">�û�Ȩ�����òִ�</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        /// <param name="userClaimRepository">�û�����ִ�</param>
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimRepository)
        {
        }
    }
}