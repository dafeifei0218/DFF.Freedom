using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.Users.Dto;
using Microsoft.AspNet.Identity;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// 用户 应用程序服务接口
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 禁止权限
        /// </summary>
        /// <param name="input">禁止权限输入模型</param>
        /// <returns></returns>
        Task ProhibitPermission(ProhibitPermissionInput input);

        /// <summary>
        /// 根据角色名称中删除指定用户Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        Task RemoveFromRole(long userId, string roleName);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<UserListDto>> GetUsers();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建用户输入模型</param>
        /// <returns></returns>
        Task CreateUser(CreateUserInput input);

        /// <summary>
        /// 获取编辑页面的用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns>获取编辑页面的用户信息 输出模型</returns>
        Task<GetUserForEditOutput> GetUserForEdit(long id);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">输入模型</param>
        /// <returns></returns>
        Task UpdateUser(UpdateUserInput input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task DeleteUser(long userId);
    }
}