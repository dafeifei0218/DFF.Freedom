using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// 禁止权限 输入模型
    /// </summary>
    public class ProhibitPermissionInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Range(1, long.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        public string PermissionName { get; set; }
    }
}