using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// ��ֹȨ������ģ��
    /// </summary>
    public class ProhibitPermissionInput
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        [Range(1, long.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// Ȩ������
        /// </summary>
        [Required]
        public string PermissionName { get; set; }
    }
}