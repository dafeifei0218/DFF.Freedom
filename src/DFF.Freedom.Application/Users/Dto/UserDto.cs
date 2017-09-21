using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// �û� �������
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        /// <summary>
        /// �û�����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ȫ����
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// �ϴε�¼ʱ��
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string[] Roles { get; set; }
    }
}