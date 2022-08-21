namespace ElectronicDiary.Services;

public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(int id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetRangeAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity);
}