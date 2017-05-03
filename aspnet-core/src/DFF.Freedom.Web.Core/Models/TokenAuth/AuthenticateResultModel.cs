namespace DFF.Freedom.Models.TokenAuth
{
    /// <summary>
    /// 认证结果 模型
    /// </summary>
    public class AuthenticateResultModel
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 加密的访问令牌
        /// </summary>
        public string EncryptedAccessToken { get; set; }

        /// <summary>
        /// 过期的秒数
        /// </summary>
        public int ExpireInSeconds { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
