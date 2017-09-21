using System.ComponentModel.DataAnnotations;

namespace DFF.Freedom.Configuration.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeUiThemeInput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Theme { get; set; }
    }
}