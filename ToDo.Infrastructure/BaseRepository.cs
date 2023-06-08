using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.Abstractions;

namespace ToDo.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public readonly DbContext _context;
        public readonly DbSet<T> _dbSet;
        public IQueryable<T> Table
        {
            get
            {
                return _dbSet;
            }
        }
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(CancellationToken cancellationToken, T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<T> GetAsync(CancellationToken cancellationToken, params object[] key)
        {
            var entity = await _dbSet.FindAsync(key,cancellationToken);
            if(entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, params object[] key)
        {
            T entity = await _dbSet.FindAsync(key, cancellationToken);
            entity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task RemoveAsync(CancellationToken cancellationToken, T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CancellationToken cancellationTokem, T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);

            await _context.SaveChangesAsync(cancellationTokem);
        }

        public async Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate)
        {
           return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
