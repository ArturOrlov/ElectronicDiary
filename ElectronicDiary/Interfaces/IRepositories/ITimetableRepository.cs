using ElectronicDiary.Dto.Timetable;
using ElectronicDiary.Entities;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Services;

namespace ElectronicDiary.Interfaces.IRepositories;

public interface ITimetableRepository
{
    Task<GetTimetableDto> GetTimetableByIdAsync(int timetableId);

    Task<List<GetTimetableDto>> GetTimetableByPaginationAsync(BasePagination request);

    Task<GetTimetableDto> CreateTimetableAsync(CreateTimetableDto request);

    Task<GetTimetableDto> UpdateTimetableByIdAsync(int timetableId, UpdateTimetableDto request);

    Task<GetTimetableDto> DeleteTimetableByIdAsync(int timetableId);
}