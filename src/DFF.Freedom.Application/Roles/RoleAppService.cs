using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Authorization;
using DFF.Freedom.Authorization.Roles;
using DFF.Freedom.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.IdentityFramework;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Authorization;
using Abp.UI;

namespace DFF.Freedom.Roles
{
	/// <summary>
    /// 角色 应用程序服务
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleManager">角色管理</param>
        /// <param name="permissionManager">权限管理</param>
        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建角色数据传输对象</param>
        /// <returns></returns>
        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">角色数据传输对象</param>
        /// <returns></returns>
        public override async Task<RoleDto> Update(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input">被删除的实体数据传输对象</param>
        /// <returns></returns>
        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            if (role.IsStatic)
            {
                throw new UserFriendlyException("CannotDeleteAStaticRole");
            }

            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <returns></returns>
        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }

        /// <summary>
        /// 创建过滤查询
        /// </summary>
        /// <param name="input">分页结果请求数据传输对象</param>
        /// <returns></returns>
        protected override IQueryable<Role> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 应用排序
        /// </summary>
        /// <param name="query">角色查询结果</param>
        /// <param name="input">分页结果请求数据传输对象</param>
        /// <returns></returns>
        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="identityResult">认证结果</param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}