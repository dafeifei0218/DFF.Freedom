using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DFF.Freedom.Authorization;
using DFF.Freedom.Controllers;
using DFF.Freedom.Users;
using DFF.Freedom.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 用户 控制器
    /// </summary>
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : FreedomControllerBase
    {
        private readonly IUserAppService _userAppService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userAppService"></param>
        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var users = (await _userAppService.GetAll(new PagedResultRequestDto {MaxResultCount = int.MaxValue})).Items; //Paging not implemented yet
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new UserListViewModel
            {
                Users = users,
                Roles = roles
            };
            return View(model);
        }

        /// <summary>
        /// 编辑用户模态窗口
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<ActionResult> EditUserModal(long userId)
        {
            var user = await _userAppService.Get(new EntityDto<long>(userId));
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserModalViewModel
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
    }
}