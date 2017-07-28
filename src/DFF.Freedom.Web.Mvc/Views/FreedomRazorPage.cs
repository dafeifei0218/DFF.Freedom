using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace DFF.Freedom.Web.Views
{
    public abstract class FreedomRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected FreedomRazorPage()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }
    }
}
