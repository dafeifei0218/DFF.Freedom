using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// 用户列表 数据传输对象
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 全名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 是否邮件确认
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}