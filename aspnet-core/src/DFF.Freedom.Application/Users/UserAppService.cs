using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users.Dto;
using DFF.Freedom.Authorization.Roles;

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
        private readonly RoleManager _roleManager;


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
            //var user = await UserManager.FindByIdAsync(userId.ToString());
            var user = await UserManager.FindByIdAsync(userId);

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
        /// ��ȡ�༭ҳ����û���Ϣ
        /// </summary>
        /// <param name="id">�û�Id</param>
        /// <returns>��ȡ�༭ҳ����û���Ϣ ���ģ��</returns>
        public async Task<GetUserForEditOutput> GetUserForEdit(long id) {
            
            GetUserForEditOutput output = new GetUserForEditOutput();
            output.User = await UserManager.GetUserByIdAsync(id);

            var roleStrings = await UserManager.GetRolesAsync(id);
            var rolesList = new List<Role>();
            foreach (var roleId in roleStrings)
            {
                var role = await _roleManager.GetRoleByIdAsync(Convert.ToInt32(roleId));
                rolesList.Add(role);
            }
            output.Roles = rolesList; 

            return output;
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        public async Task UpdateUser(UpdateUserInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            user.Name = input.Name;
            user.Surname = input.Surname;
            user.Password = input.Password;

            CheckErrors(await UserManager.UpdateAsync(user));
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public async Task DeleteUser(long userId)
        {
            var user = await UserManager.GetUserByIdAsync(userId);

            CheckErrors(await UserManager.DeleteAsync(user));
        }
    }
}