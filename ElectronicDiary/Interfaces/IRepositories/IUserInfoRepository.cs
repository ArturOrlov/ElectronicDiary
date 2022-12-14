using ElectronicDiary.Entities.DbModels;

namespace ElectronicDiary.Interfaces.IRepositories;

public interface IUserInfoRepository : IGenericRepository<UserInfo>
{
    Task<UserInfo> GetByUserId(int userId);
}