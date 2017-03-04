using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : FreedomControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}