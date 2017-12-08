using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.Users.Dto;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// �û� Ӧ�ó������ӿ�
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// ��ֹȨ��
        /// </summary>
        /// <param name="input">��ֹȨ������ģ��</param>
        /// <returns></returns>
        Task ProhibitPermission(ProhibitPermissionInput input);

        /// <summary>
        /// ���ݽ�ɫ������ɾ��ָ���û�Id
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="roleName">��ɫ����</param>
        /// <returns></returns>
        Task RemoveFromRole(long userId, string roleName);

        /// <summary>
        /// ��ȡ�û��б�
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<UserListDto>> GetUsers();

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input">�����û�����ģ��</param>
        /// <returns></returns>
        Task CreateUser(CreateUserInput input);

        /// <summary>
        /// ��ȡ�༭ҳ����û���Ϣ
        /// </summary>
        /// <param name="id">�û�Id</param>
        /// <returns>��ȡ�༭ҳ����û���Ϣ ���ģ��</returns>
        Task<GetUserForEditOutput> GetUserForEdit(long id);

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input">����ģ��</param>
        /// <returns></returns>
        Task UpdateUser(UpdateUserInput input);

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        Task DeleteUser(long userId);
    }
}