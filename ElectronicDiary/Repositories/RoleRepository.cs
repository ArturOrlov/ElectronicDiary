using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class RoleRepository :  GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}