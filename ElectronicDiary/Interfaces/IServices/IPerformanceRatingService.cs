using ElectronicDiary.Dto.PerformanceRating;
using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.User;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDiary.Interfaces.IServices;

public interface IPerformanceRatingService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="performanceRatingId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetPerformanceRatingDto>> GetPerformanceRatingByIdAsync(int performanceRatingId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetPerformanceRatingDto>>> GetPerformanceRatingBySelfAsync(UserDataDto userData);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<ResponsePerformanceRatingReportDto>>> GetPerformanceRatingReportAsync(GetPerformanceRatingReportDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    Task<BaseResponse<FileContentResult>> GetReportPerformanceRatingBySelfAsync(UserDataDto userData);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetPerformanceRatingDto>> CreatePerformanceRatingAsync(CreatePerformanceRatingDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="performanceRatingId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetPerformanceRatingDto>> UpdatePerformanceRatingByIdAsync(int performanceRatingId, UpdatePerformanceRatingDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="performanceRatingId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeletePerformanceRatingByIdAsync(int performanceRatingId);
}