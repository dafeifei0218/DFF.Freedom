using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using DFF.Freedom.Editions;
using DFF.Freedom.Users;

namespace DFF.Freedom.MultiTenancy
{
    /// <summary>
    /// 租户管理
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantRepository">租户仓储</param>
        /// <param name="tenantFeatureRepository">租户特征仓储</param>
        /// <param name="editionManager">版本管理</param>
        /// <param name="featureValueStore">特征值存储</param>
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}