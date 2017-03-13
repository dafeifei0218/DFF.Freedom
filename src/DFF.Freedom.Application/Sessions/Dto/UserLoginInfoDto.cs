using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DFF.Freedom.Users;

namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户姓氏
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 用户名称。用户名在租户必须是唯一。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮件地址
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
