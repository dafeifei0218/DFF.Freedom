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
    /// 用户 应用程序服务类
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : FreedomAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly RoleManager _roleManager;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="permissionManager">权限管理</param>
        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// 禁止权限
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            //根据Id，获取用户信息
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            //根据权限名称，获取权限信息
            var permission = _permissionManager.GetPermission(input.PermissionName);

            //禁止指定用户的权限
            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        //原始方法参数示例。
        /// <summary>
        /// 根据角色名称中删除指定用户Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public async Task RemoveFromRole(long userId, string roleName)
        {
            //var user = await UserManager.FindByIdAsync(userId.ToString());
            var user = await UserManager.FindByIdAsync(userId);

            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            //获取用户全部列表
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">输入模型</param>
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
        /// 获取编辑页面的用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns>获取编辑页面的用户信息 输出模型</returns>
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
        /// 更新用户
        /// </summary>
        /// <param name="input">输入模型</param>
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
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task DeleteUser(long userId)
        {
            var user = await UserManager.GetUserByIdAsync(userId);

            CheckErrors(await UserManager.DeleteAsync(user));
        }
    }
}