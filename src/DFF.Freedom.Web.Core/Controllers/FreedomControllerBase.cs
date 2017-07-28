using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

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