using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DFF.Freedom.Authorization.Users;

namespace DFF.Freedom.Users.Dto
{
    /// <summary>
    /// �û��б� ���ݴ������
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// ��¼�û���
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ȫ����
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// �Ƿ��ʼ�ȷ��
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// �ϴε�¼ʱ��
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}