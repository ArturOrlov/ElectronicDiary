using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IUserService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserDto>> GetUserByIdAsync(int userId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetUserDto>>> GetUserByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserDto>> CreateUserAsync(CreateUserDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserDto>> UpdateUserByIdAsync(int userId, UpdateUserDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteUserByIdAsync(int userId);
}