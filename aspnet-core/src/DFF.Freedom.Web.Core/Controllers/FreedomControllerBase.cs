using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class FreedomControllerBase: AbpController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected FreedomControllerBase()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult">认证结果</param>
        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}