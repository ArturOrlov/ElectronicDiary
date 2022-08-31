using ElectronicDiary.Entities.DbModels;

namespace ElectronicDiary.Interfaces.IRepositories;

public interface IUserClassRepository : IGenericRepository<UserClass>
{
    Task<UserClass> GetByUserId(int userId);
}