using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class CabinetRepository : GenericRepository<Cabinet>, ICabinetRepository
{
    public CabinetRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}