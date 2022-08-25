using ElectronicDiary.Dto.Cabinet;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface ICabinetService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cabinetId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetCabinetDto>> GetCabinetByIdAsync(int cabinetId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetCabinetDto>>> GetCabinetByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetCabinetDto>> CreateCabinetAsync(CreateCabinetDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cabinetId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetCabinetDto>> UpdateCabinetByIdAsync(int cabinetId, UpdateCabinetDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cabinetId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteCabinetByIdAsync(int cabinetId);
}