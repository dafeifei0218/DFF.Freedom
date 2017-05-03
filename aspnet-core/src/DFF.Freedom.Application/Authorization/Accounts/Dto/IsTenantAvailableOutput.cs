namespace DFF.Freedom.Authorization.Accounts.Dto
{
    /// <summary>
    /// �Ƿ��⻧���� ���ģ��
    /// </summary>
    public class IsTenantAvailableOutput
    {
        /// <summary>
        /// �⻧����״̬
        /// </summary>
        public TenantAvailabilityState State { get; set; }

        /// <summary>
        /// �⻧Id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public IsTenantAvailableOutput()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="state">�⻧����״̬</param>
        /// <param name="tenantId">�⻧Id</param>
        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
        {
            State = state;
            TenantId = tenantId;
        }
    }
}