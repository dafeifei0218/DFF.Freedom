using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DFF.Freedom.Roles.Dto;
using DFF.Freedom.Users.Dto;

namespace DFF.Freedom.Users
{
    /// <summary>
    /// 用户 应用程序服务接口
    /// </summary>
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns>返回 角色数据传输对象列表</returns>
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}