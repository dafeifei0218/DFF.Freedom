namespace DFF.Freedom.Authorization.Accounts.Dto
{
    /// <summary>
    /// 租户可用状态枚举
    /// </summary>
    public enum TenantAvailabilityState
    {
        /// <summary>
        /// 可用
        /// </summary>
        Available = 1,
        /// <summary>
        /// 已激活
        /// </summary>
        InActive,
        /// <summary>
        /// 未找到
        /// </summary>
        NotFound
    }
}