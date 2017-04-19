using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    /// <summary>
    /// 关于 控制器
    /// </summary>
    public class AboutController : FreedomControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}