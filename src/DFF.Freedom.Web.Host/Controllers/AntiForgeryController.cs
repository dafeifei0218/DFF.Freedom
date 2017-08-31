using DFF.Freedom.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace DFF.Freedom.Web.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AntiForgeryController : FreedomControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="antiforgery"></param>
        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}