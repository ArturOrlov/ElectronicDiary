using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.Base;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using ElectronicDiary.Interfaces.IServices;
using ElectronicDiary.Services;

namespace ElectronicDiary.Repositories;

public class TimetableRepository : ITimetableRepository
{
    public async Task<GetTimetableDto> GetTimetableByIdAsync(int timetableId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GetTimetableDto>> GetTimetableByPaginationAsync(BasePagination request)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTimetableDto> CreateTimetableAsync(CreateTimetableDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTimetableDto> UpdateTimetableByIdAsync(int timetableId, UpdateTimetableDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTimetableDto> DeleteTimetableByIdAsync(int timetableId)
    {
        throw new NotImplementedException();
    }
}