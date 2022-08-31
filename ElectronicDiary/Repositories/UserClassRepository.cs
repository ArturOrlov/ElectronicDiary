using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Repositories;

public class UserClassRepository : GenericRepository<UserClass>, IUserClassRepository
{
    public UserClassRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }

    public async Task<UserClass> GetByUserId(int userId)
    {
        return await _context.UserClass.FirstOrDefaultAsync(ui => ui.UserId == userId);
    }
}