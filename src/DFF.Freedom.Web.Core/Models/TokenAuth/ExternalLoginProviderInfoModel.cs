using Abp.AutoMapper;
using DFF.Freedom.Authentication.External;

namespace DFF.Freedom.Models.TokenAuth
{
    /// <summary>
    /// 外部登录提供信息 模型
    /// </summary>
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }
    }
}
