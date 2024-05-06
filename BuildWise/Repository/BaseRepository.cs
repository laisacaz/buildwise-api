using BuildWise.Interface.Repository;
using Dommel;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace BuildWise.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected IDbConnection _conn;
        protected IDbTransaction _transaction;

        private static readonly Func<TEntity> NewEntityFunc = Expression.Lambda<Func<TEntity>>(
          Expression.New(typeof(TEntity).GetConstructors()[0])).Compile();
        protected BaseRepository(IDbConnection conn, IDbTransaction transaction)
        {
            _conn = conn;
            _transaction = transaction;
        }
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            object id = await _conn.InsertAsync(entity, _transaction);
            if (id is null)
                return 0;
            return int.Parse(id.ToString());
        }
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _conn.GetAsync<TEntity>(id, _transaction);
        }
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _conn.DeleteAsync(entity, _transaction);
        }
        public virtual async Task InsertAllAsync(IEnumerable<TEntity> entityList)
        {
            await _conn.InsertAllAsync(entityList, _transaction);
        }
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _conn.UpdateAsync(entity, _transaction);
        }
        public virtual async Task<List<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> data = await _conn.SelectAsync(predicate, _transaction);
            return data.ToList();
        }
    }
}
