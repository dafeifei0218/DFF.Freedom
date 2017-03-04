using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using Abp.IdentityFramework;

namespace DFF.Freedom.Web.Controllers
{
    public abstract class FreedomControllerBase: AbpController
    {
        protected FreedomControllerBase()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}