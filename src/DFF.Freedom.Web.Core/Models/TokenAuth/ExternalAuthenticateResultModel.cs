namespace DFF.Freedom.Models.TokenAuth
{
    /// <summary>
    /// 外部认证结果 模型
    /// </summary>
    public class ExternalAuthenticateResultModel
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
        /// 等待激活
        /// </summary>
        public bool WaitingForActivation { get; set; }
    }
}
