using ElectronicDiary.Dto.Report;
using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;

namespace ElectronicDiary.Interfaces.IServices;

public interface ISubjectService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSubjectDto>> GetSubjectByIdAsync(int subjectId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<GetSubjectDto>>> GetSubjectByPaginationAsync(BasePagination request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<List<ResponseSubjectReportDto>>> GetSubjectReportAsync(GetSubjectReportDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSubjectDto>> CreateSubjectAsync(CreateSubjectDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subjectId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<BaseResponse<GetSubjectDto>> UpdateSubjectByIdAsync(int subjectId, UpdateSubjectDto request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    Task<BaseResponse<string>> DeleteSubjectByIdAsync(int subjectId);
}