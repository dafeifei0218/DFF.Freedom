﻿using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace DFF.Freedom.Editions
{
    /// <summary>
    /// 版本管理
    /// </summary>
    public class EditionManager : AbpEditionManager
    {
        /// <summary>
        /// 默认版本名称
        /// </summary>
        public const string DefaultEditionName = "Standard";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="editionRepository">版本仓储</param>
        /// <param name="featureValueStore">特征值存储</param>
        public EditionManager(
            IRepository<Edition> editionRepository, 
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore
            )
        {
        }
    }
}
