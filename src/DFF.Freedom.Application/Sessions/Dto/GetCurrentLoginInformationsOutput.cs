namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 获取当前登录信息 输出模型
    /// </summary>
    public class GetCurrentLoginInformationsOutput
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfoDto Application { get; set; }


        /// <summary>
        /// 用户登录信息
        /// </summary>
        public UserLoginInfoDto User { get; set; }

        /// <summary>
        /// 租主登录信息
        /// </summary>
        public TenantLoginInfoDto Tenant { get; set; }
    }
}