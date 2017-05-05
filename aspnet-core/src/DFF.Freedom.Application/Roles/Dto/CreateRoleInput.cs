using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DFF.Freedom.Authorization.Roles;
using Abp.AutoMapper;

namespace DFF.Freedom.Roles.Dto
{
    /// <summary>
    /// 创建租户 输入模型
    /// </summary>
    //[AutoMapTo(typeof(Role))]
    public class CreateRoleInput 
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(Role.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [StringLength(Role.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
