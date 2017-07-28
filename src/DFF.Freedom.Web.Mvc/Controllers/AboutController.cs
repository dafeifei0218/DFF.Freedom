using DFF.Freedom.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DFF.Freedom.Web.Controllers
{
    public class AboutController : FreedomControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}