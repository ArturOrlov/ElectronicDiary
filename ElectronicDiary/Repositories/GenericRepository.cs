using ElectronicDiary.Context;
using ElectronicDiary.Entities;
using ElectronicDiary.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ElectronicDiaryDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ElectronicDiaryDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetRangeAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate, BasePagination pagination)
    {
        return _dbSet.Where(predicate)
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToList();
    }

    public async Task CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}