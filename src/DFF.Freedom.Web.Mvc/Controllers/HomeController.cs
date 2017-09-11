using Abp.AspNetCore.Mvc.Authorization;
using DFF.Freedom.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 主页 控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class HomeController : FreedomControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}