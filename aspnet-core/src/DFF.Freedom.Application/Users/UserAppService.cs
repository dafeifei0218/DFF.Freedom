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
    /// 用户服务类
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : FreedomAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="permissionManager"></param>
        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        //原始方法参数示例。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
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