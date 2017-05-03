namespace DFF.Freedom.Authorization.Accounts.Dto
{
    /// <summary>
    /// 是否租户可用 输出模型
    /// </summary>
    public class IsTenantAvailableOutput
    {
        /// <summary>
        /// 租户可用状态
        /// </summary>
        public TenantAvailabilityState State { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IsTenantAvailableOutput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="state">租户可用状态</param>
        /// <param name="tenantId">租户Id</param>
        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
        {
            State = state;
            TenantId = tenantId;
        }
    }
}