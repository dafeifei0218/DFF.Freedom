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
    /// ����ֵ�洢
    /// </summary>
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, User>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheManager">�������</param>
        /// <param name="tenantFeatureRepository">�⻧�����ִ�</param>
        /// <param name="tenantRepository">�⻧�ִ�</param>
        /// <param name="editionFeatureRepository">�汾�����ִ�</param>
        /// <param name="featureManager">��������</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
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