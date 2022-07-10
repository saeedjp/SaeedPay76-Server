using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SaeedPay76.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(object id);
        void Delete(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetWithFilterAsync(Expression<Func<TEntity, bool>> expression = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            string includeEntity = "");
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> expression);
    }
}
