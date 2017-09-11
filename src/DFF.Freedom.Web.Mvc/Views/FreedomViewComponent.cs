using Abp.AspNetCore.Mvc.ViewComponents;

namespace DFF.Freedom.Web.Views
{
    /// <summary>
    /// Freedom视图组件基类
    /// </summary>
    public abstract class FreedomViewComponent : AbpViewComponent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected FreedomViewComponent()
        {
            // 本地化资源名称
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }
    }
}