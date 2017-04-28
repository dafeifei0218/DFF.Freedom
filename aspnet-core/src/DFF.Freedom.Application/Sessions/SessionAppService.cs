﻿using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Sessions
{
    /// <summary>
    /// Session服务类
    /// </summary>
    public class SessionAppService : FreedomAppServiceBase, ISessionAppService
    {
        /// <summary>
        /// 获取当前登录信息
        /// </summary>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>();
            }

            return output;
        }
    }
}