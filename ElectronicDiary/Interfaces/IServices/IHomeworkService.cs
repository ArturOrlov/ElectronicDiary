using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface IHomeworkService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="homeworkId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetHomeworkDto>> GetHomeworkByIdAsync(int homeworkId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetHomeworkDto>>> GetHomeworkByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetHomeworkDto>> CreateHomeworkAsync(CreateHomeworkDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="homeworkId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetHomeworkDto>> UpdateHomeworkByIdAsync(int homeworkId, UpdateHomeworkDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="homeworkId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteHomeworkByIdAsync(int homeworkId);
}