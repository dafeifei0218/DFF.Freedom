namespace DFF.Freedom.Authentication.External
{
    /// <summary>
    /// 外部认证用户信息
    /// </summary>
    public class ExternalAuthUserInfo
    {
        /// <summary>
        /// 提供者的键
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 真是名称
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 提供者
        /// </summary>
        public string Provider { get; set; }
    }
}