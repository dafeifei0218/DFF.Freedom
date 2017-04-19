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
        /// �û�����
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// �����ַ
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// ���ƺ���������
        /// </summary>
        public string NameAndSurname { get; set; }

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }
    }
}