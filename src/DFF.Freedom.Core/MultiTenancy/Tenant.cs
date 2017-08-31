using Abp.MultiTenancy;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Tenant()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenancyName">租户名称</param>
        /// <param name="name">租户显示名称</param>
        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}