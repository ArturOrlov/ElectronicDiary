using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;

namespace ElectronicDiary.Services;

public class TimetableService : ITimetableService
{
    private readonly ITimetableRepository _timetableRepository;

    public TimetableService(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<BaseResponse<GetTimetableDto>> GetTimetableByIdAsync(int timetableId)
    {
        var response = new BaseResponse<GetTimetableDto>();

        var timetable = await _timetableRepository.GetTimetableByIdAsync(timetableId);

        if (timetable == null)
        {
            response.Description = "хуй";
            response.IsError = true;
            return response;
        }

        response.Data = timetable;
        return response;
    }

    public async Task<BaseResponse<List<GetTimetableDto>>> GetTimetableByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetTimetableDto>> CreateTimetableAsync(CreateTimetableDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetTimetableDto>> UpdateTimetableByIdAsync(int timetableId, UpdateTimetableDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetTimetableDto>> DeleteTimetableByIdAsync(int timetableId)
    {
        throw new NotImplementedException();
    }
}