namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// ע���� ��ͼģ��
    /// </summary>
    public class RegisterResultViewModel
    {
        /// <summary>
        /// �⻧����
        /// </summary>
        public string TenancyName { get; set; }
        
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// ���ƺ���ʵ����
        /// </summary>
        public string NameAndSurname { get; set; }

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ��¼ʱ�Ƿ���Ҫ�����ʼ�ȷ��
        /// </summary>
        public bool IsEmailConfirmationRequiredForLogin { get; set; }

        /// <summary>
        /// �Ƿ��ʼ�ȷ��
        /// </summary>
        public bool IsEmailConfirmed { get; set; }
    }
}