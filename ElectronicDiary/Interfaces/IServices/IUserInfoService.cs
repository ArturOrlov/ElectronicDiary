using ElectronicDiary.Dto.UserInfo;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IUserInfoService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserInfoDto>> GetByIdAsync(int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserInfoDto>> CreateAsync(int userId, CreateUserInfoDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetUserInfoDto>> UpdateByIdAsync(int userId, UpdateUserInfoDto request);
}