using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface ISchoolClassService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="schoolClassIdId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSchoolClassDto>> GetSchoolClassByIdAsync(int schoolClassIdId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetSchoolClassDto>>> GetSchoolClassByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSchoolClassDto>> CreateSchoolClassAsync(CreateSchoolClassDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="schoolClassIdId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSchoolClassDto>> UpdateSchoolClassByIdAsync(int schoolClassIdId, UpdateSchoolClassDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="schoolClassIdId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteSchoolClassByIdAsync(int schoolClassIdId);
}