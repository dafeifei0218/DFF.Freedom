using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Configuration.Dto
{
    /// <summary>
    /// 改变UI主题 输入模型
    /// </summary>
    public class ChangeUiThemeInput
    {
        /// <summary>
        /// 主题
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Theme { get; set; }
    }
}