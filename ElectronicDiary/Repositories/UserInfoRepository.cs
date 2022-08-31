using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Repositories;

public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
{
    public UserInfoRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }

    public async Task<UserInfo> GetByUserId(int userId)
    {
        return await _context.UserInfo.FirstOrDefaultAsync(ui => ui.UserId == userId);
    }
}