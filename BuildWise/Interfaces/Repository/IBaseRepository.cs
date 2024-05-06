using System.Linq.Expressions;

namespace BuildWise.Interface.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<int> InsertAsync(TEntity entity);
        Task InsertAllAsync(IEnumerable<TEntity> entityList);
        Task<bool> UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(object id);
        Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
