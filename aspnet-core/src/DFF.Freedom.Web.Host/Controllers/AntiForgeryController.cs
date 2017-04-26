using DFF.Freedom.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace DFF.Freedom.Web.Host.Controllers
{
    public class AntiForgeryController : FreedomControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}