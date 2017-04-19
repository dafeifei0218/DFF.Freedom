using System.Collections.Generic;
using Abp.AutoMapper;
using DFF.Freedom.MultiTenancy;

namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 租户选择 视图模型
    /// </summary>
    public class TenantSelectionViewModel
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 返回链接
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 认证方案
        /// </summary>
        public string AuthSchema { get; set; }

        /// <summary>
        /// 租户列表
        /// </summary>
        public List<TenantInfo> Tenants { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        [AutoMapFrom(typeof(Tenant))]
        public class TenantInfo
        {
            /// <summary>
            /// 主键
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 租户名称
            /// </summary>
            public string TenancyName { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }
        }
    }
}