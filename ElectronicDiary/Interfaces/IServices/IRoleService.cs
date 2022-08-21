using ElectronicDiary.Dto.Role;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IRoleService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetRoleDto>> GetRoleByIdAsync(int roleId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetRoleDto>>> GetRoleByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetRoleDto>> CreateRoleAsync(CreateRoleDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetRoleDto>> UpdateRoleByIdAsync(int roleId, UpdateRoleDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetRoleDto>> DeleteRoleByIdAsync(int roleId);
}