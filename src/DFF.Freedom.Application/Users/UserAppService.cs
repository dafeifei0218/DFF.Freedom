using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DFF.Freedom.Authorization;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.IdentityFramework;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Roles.Dto;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// 用户 应用程序服务类
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Role> _roleRepository;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="permissionManager">权限管理</param>
        public UserAppService(IRepository<User, long> repository, UserManager userManager, IPasswordHasher<User> passwordHasher, IRepository<Role> roleRepository)
            : base(repository)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }
        
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建用户数据传输对象</param>
        /// <returns></returns>
        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission(); //检查创建权限

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await _userManager.CreateAsync(user));

            if (input.Roles != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.Roles));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">用户传输对象</param>
        /// <returns></returns>
        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission(); //检查更新权限

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.Roles != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.Roles));
            }

            return await Get(input);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input">被删除的实体数据传输对象</param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
		}

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="createInput">创建用户数据传输对象</param>
        /// <returns></returns>
        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="input">用户数据传输对象</param>
        /// <param name="user">用户实体</param>
        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }
        
        /// <summary>
        /// 创建过滤查询
        /// </summary>
        /// <param name="input">分页结果请求数据传输对象</param>
        /// <returns></returns>
        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        /// <summary>
        /// 根据Id获取实体 异步方法
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            return await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 应用排序
        /// </summary>
        /// <param name="query">用户查询结果</param>
        /// <param name="input">分页结果请求数据传输对象</param>
        /// <returns></returns>
        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult">认证及结果</param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}