using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Controllers
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