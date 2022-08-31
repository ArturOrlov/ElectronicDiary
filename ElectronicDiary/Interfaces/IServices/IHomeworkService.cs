using ElectronicDiary.Dto.Homework;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using Microsoft.AspNetCore.Mvc;

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
    /// <param name="userData"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetHomeworkDto>>> GetHomeworkBySelfAsync(UserDataDto userData);   
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    Task<BaseResponse<FileContentResult>> GetReportHomeworkBySelfAsync(UserDataDto userData);
    
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