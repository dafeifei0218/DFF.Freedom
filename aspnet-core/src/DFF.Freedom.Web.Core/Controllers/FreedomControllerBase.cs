using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Controllers
{
    /// <summary>
    /// ����������
    /// </summary>
    public abstract class FreedomControllerBase: AbpController
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        protected FreedomControllerBase()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="identityResult">��֤���</param>
        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}