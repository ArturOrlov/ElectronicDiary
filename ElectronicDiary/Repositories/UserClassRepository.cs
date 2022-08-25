using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class UserClassRepository : GenericRepository<UserClass>, IUserClassRepository
{
    public UserClassRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}