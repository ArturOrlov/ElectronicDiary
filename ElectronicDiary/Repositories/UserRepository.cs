using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}