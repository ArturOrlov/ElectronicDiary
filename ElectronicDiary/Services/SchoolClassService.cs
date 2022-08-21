using ElectronicDiary.Dto.SchoolClass;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class SchoolClassService : ISchoolClassService
{
    private readonly ISchoolClassRepository _schoolClassRepository;

    public SchoolClassService(ISchoolClassRepository schoolClassRepository)
    {
        _schoolClassRepository = schoolClassRepository;
    }

    public async Task<BaseResponse<GetSchoolClassDto>> GetSchoolClassByIdAsync(int schoolClassIdId)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<GetSchoolClassDto>>> GetSchoolClassByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSchoolClassDto>> CreateSchoolClassAsync(CreateSchoolClassDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSchoolClassDto>> UpdateSchoolClassByIdAsync(int schoolClassIdId, UpdateSchoolClassDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetSchoolClassDto>> DeleteSchoolClassByIdAsync(int schoolClassIdId)
    {
        throw new NotImplementedException();
    }
}