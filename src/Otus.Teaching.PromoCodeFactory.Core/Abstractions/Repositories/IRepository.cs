using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T>
        where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> GetByIdsAsync(params Guid[] ids);

        Task<IEnumerable<T>> GetByPredicate(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(Guid id);
        
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}