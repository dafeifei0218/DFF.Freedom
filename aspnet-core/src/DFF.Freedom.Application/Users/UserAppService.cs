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
    /// 用户 应用程序服务类
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : FreedomAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

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
    }
}