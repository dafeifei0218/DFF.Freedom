namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 获取当前登录信息输出模型
    /// </summary>
    public class GetCurrentLoginInformationsOutput
    {
        /// <summary>
        /// 获取或设置用户信息
        /// </summary>
        public UserLoginInfoDto User { get; set; }

        /// <summary>
        /// 获取或设置租主信息
        /// </summary>
        public TenantLoginInfoDto Tenant { get; set; }
    }
}