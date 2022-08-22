using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Interfaces.IRepositories;

namespace ElectronicDiary.Repositories;

public class PerformanceRatingRepository : GenericRepository<PerformanceRating>, IPerformanceRatingRepository
{
    public PerformanceRatingRepository(ElectronicDiaryDbContext context) : base(context)
    {
    }
}