using Abp.Extensions;
using System.Text.RegularExpressions;

namespace DFF.Freedom.Validation
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Email正则表达式
        /// </summary>
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 是否Email
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            var regex = new Regex(EmailRegex);
            return regex.IsMatch(value);
        }
    }
}
