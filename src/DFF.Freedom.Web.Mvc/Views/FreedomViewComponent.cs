using Abp.AspNetCore.Mvc.ViewComponents;

namespace DFF.Freedom.Web.Views
{
    /// <summary>
    /// Freedom视图组件基类
    /// </summary>
    public abstract class FreedomViewComponent : AbpViewComponent
    {
        protected FreedomViewComponent()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }
    }
}