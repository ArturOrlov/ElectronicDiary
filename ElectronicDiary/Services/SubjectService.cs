using ElectronicDiary.Dto.Subject;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<BaseResponse<GetSubjectDto>> GetSubjectByIdAsync(int subjectId)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<GetSubjectDto>>> GetSubjectByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSubjectDto>> CreateSubjectAsync(CreateSubjectDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSubjectDto>> UpdateSubjectByIdAsync(int subjectId, UpdateSubjectDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSubjectDto>> DeleteSubjectByIdAsync(int subjectId)
    {
        throw new NotImplementedException();
    }
}