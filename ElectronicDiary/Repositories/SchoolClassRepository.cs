using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
{
    public SchoolClassRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}