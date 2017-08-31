using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Roles;

using Abp.Authorization.Roles;

namespace DFF.Freedom.Roles.Dto
{
    /// <summary>
    /// 创建租户 输入模型
    /// </summary>
    [AutoMapTo(typeof(Role))]
    public class CreateRoleDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }
        
        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }
        
        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsStatic { get; set; }

        public List<string> Permissions { get; set; }
    }
}