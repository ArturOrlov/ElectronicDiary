using ElectronicDiary.Dto.UserRole;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IUserRoleService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userRoleId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserRoleDto>> GetUserRoleByIdAsync(int userRoleId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserRoleDto>> GetUserRoleByUserIdAsync(int userId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetUserRoleDto>>> GetUserRoleByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserRoleDto>> CreateUserRoleAsync(CreateUserRoleDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userRoleId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserRoleDto>> UpdateUserRoleByIdAsync(int userRoleId, UpdateUserRoleDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userRoleId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteUserRoleByIdAsync(int userRoleId);
}