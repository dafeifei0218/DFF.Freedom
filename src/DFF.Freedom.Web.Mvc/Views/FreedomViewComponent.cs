using Abp.AspNetCore.Mvc.ViewComponents;

namespace DFF.Freedom.Web.Views
{
    public abstract class FreedomViewComponent : AbpViewComponent
    {
        protected FreedomViewComponent()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }
    }
}