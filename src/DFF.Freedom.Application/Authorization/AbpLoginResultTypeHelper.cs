using System;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.UI;

namespace DFF.Freedom.Authorization
{
    /// <summary>
    /// Abp登录结果类型 帮助类
    /// </summary>
    public class AbpLoginResultTypeHelper : AbpServiceBase, ITransientDependency
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AbpLoginResultTypeHelper()
        {
            LocalizationSourceName = FreedomConsts.LocalizationSourceName;
        }
        
        /// <summary>
        /// 为失败的登录尝试创建异常。
        /// </summary>
        /// <param name="result">Abp登录结果类型</param>
        /// <param name="usernameOrEmailAddress">用户名或电子邮件地址</param>
        /// <param name="tenancyName">租户名称</param>
        /// <returns></returns>
        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success: //成功
                    return new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress: //无效的用户名或电子邮件地址
                case AbpLoginResultType.InvalidPassword: //密码无效
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName: //租户名不存在
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive: //租户未激活
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive: //用户未激活
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed: //用户电子邮件未确认
                    return new UserFriendlyException(L("LoginFailed"), L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut: //锁定
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                         //不能到默认。但其他的结果类型可以添加在未来，我们可能会忘记处理它。
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        /// <summary>
        /// 为失败的登录尝试创建本地化消息。
        /// </summary>
        /// <param name="result">Abp登录结果类型</param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="tenancyName">租户名称</param>
        /// <returns></returns>
        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return L("InvalidUserNameOrPassword");
                case AbpLoginResultType.InvalidTenancyName:
                    return L("ThereIsNoTenantDefinedWithName{0}", tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L("TenantIsNotActive", tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L("UserEmailIsNotConfirmedAndCanNotLogin");
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return L("LoginFailed");
            }
        }
    }
}
