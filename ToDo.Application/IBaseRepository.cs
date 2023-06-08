using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Abstractions;

namespace ToDo.Application
{
    public interface IBaseRepository<T>where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetAsync(CancellationToken cancellationToken, params object[] key);
        Task AddAsync(CancellationToken cancellationToken, T entity);
        Task RemoveAsync(CancellationToken cancellationToken, params object[] key);
        Task RemoveAsync(CancellationToken cancellationToken, T entity);
        Task UpdateAsync(CancellationToken cancellationTokem, T entity);
        Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
        IQueryable<T> Table { get; }
    }
}
