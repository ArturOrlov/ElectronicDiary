using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDiary.Interfaces.IServices;

public interface ITimetableService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timetableId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetTimetableDto>> GetTimetableByIdAsync(int timetableId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// Получить расписание по дате
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByTimeAsync(DateTimeOffset date);

    /// <summary>
    /// Получить расписание по дате в виде excel файла
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    Task<BaseResponse<FileContentResult>> GetTimetableByTimeExcelAsync(DateTimeOffset date);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetTimetableDto>> CreateTimetableAsync(CreateTimetableDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timetableId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetTimetableDto>> UpDateTimeOffsettableByIdAsync(int timetableId, UpDateTimeOffsettableDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timetableId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteTimetableByIdAsync(int timetableId);
}