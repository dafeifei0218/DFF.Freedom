using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DFF.Freedom.Authorization;
using DFF.Freedom.Controllers;
using DFF.Freedom.Roles;
using DFF.Freedom.Web.Models.Roles;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 角色 控制器
    /// </summary>
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : FreedomControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleAppService"></param>
        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var roles = (await _roleAppService.GetAll(new PagedAndSortedResultRequestDto())).Items;
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            var model = new RoleListViewModel
            {
                Roles = roles,
                Permissions = permissions
            };

            return View(model);
        }

        /// <summary>
        /// 编辑角色模态窗口
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public async Task<ActionResult> EditRoleModal(int roleId)
        {
            var role = await _roleAppService.Get(new EntityDto(roleId));
            var permissions = (await _roleAppService.GetAllPermissions()).Items;
            var model = new EditRoleModalViewModel
            {
                Role = role,
                Permissions = permissions
            };
            return View("_EditRoleModal", model);
        }
    }
}