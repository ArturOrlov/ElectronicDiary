using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class HomeworkRepository : GenericRepository<Homework>, IHomeworkRepository
{
    public HomeworkRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}