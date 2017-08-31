using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;
using DFF.Freedom.Users;

namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 用户登录信息 数据传输对象
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
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
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
