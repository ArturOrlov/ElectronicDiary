using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class TimetableRepository : GenericRepository<Timetable>, ITimetableRepository
{
    public TimetableRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}