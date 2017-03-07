using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using DFF.Freedom.MultiTenancy;
using DFF.Freedom.Users;

namespace DFF.Freedom.Features
{
    /// <summary>
    /// 特征值存储
    /// </summary>
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="tenantFeatureRepository">租户特征仓储</param>
        /// <param name="tenantRepository">租户仓储</param>
        /// <param name="editionFeatureRepository">版本特征仓储</param>
        /// <param name="featureManager">特征管理</param>
        /// <param name="unitOfWorkManager">工作单元管理</param>
        public FeatureValueStore(
            ICacheManager cacheManager, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            IRepository<Tenant> tenantRepository, 
            IRepository<EditionFeatureSetting, long> editionFeatureRepository, 
            IFeatureManager featureManager, 
            IUnitOfWorkManager unitOfWorkManager) 
            : base(cacheManager, 
                  tenantFeatureRepository, 
                  tenantRepository, 
                  editionFeatureRepository, 
                  featureManager, 
                  unitOfWorkManager)
        {
        }
    }
}