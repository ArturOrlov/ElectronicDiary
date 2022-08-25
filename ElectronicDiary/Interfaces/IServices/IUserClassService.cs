using ElectronicDiary.Dto.UserClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IUserClassService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userClassId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserClassDto>> GetUserClassByIdAsync(int userClassId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetUserClassDto>>> GetUserClassByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserClassDto>> CreateUserClassAsync(CreateUserClassDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userClassId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserClassDto>> UpdateUserClassByIdAsync(int userClassId, UpdateUserClassDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userClassId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteUserClassByIdAsync(int userClassId);
}