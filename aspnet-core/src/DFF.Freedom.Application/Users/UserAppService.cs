using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users.Dto;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Users
{
    /* THIS IS JUST A SAMPLE. */
    /// <summary>
    /// �û� Ӧ�ó��������
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : FreedomAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userRepository">�û��ִ�</param>
        /// <param name="permissionManager">Ȩ�޹���</param>
        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// ��ֹȨ��
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            //����Id����ȡ�û���Ϣ
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            //����Ȩ�����ƣ���ȡȨ����Ϣ
            var permission = _permissionManager.GetPermission(input.PermissionName);

            //��ָֹ���û���Ȩ��
            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        //ԭʼ��������ʾ����
        /// <summary>
        /// ���ݽ�ɫ������ɾ��ָ���û�Id
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="roleName">��ɫ����</param>
        /// <returns></returns>
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        /// <summary>
        /// ��ȡ�û��б�
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            //��ȡ�û�ȫ���б�
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await UserManager.CreateAsync(user));
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUser(UpdateUserInput input)
        {
            var user = input.MapTo<User>();

            return await UserManager.UpdateAsync(user);     
        }
    }
}