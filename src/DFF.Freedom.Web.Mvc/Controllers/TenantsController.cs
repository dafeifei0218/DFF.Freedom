using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DFF.Freedom.Authorization;
using DFF.Freedom.Controllers;
using DFF.Freedom.MultiTenancy;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 租户 控制器
    /// </summary>
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : FreedomControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantAppService"></param>
        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var output = await _tenantAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue }); //Paging not implemented yet
            return View(output);
        }

        /// <summary>
        /// 编辑租户模态窗口
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditTenantModal(int tenantId)
        {
            var tenantDto = await _tenantAppService.Get(new EntityDto(tenantId));
            return View("_EditTenantModal", tenantDto);
        }
    }
}