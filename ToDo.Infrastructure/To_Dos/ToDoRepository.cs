using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Application.To_Dos;
using ToDo.Domain.Subtasks;
using ToDo.Domain.To_Dos;

namespace ToDo.Infrastructure.To_Dos
{
    public class ToDoRepository : IToDoRepository
    {
        private IBaseRepository<To_Do> _repo;

        public ToDoRepository(IBaseRepository<To_Do> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(CancellationToken cancellationToken, To_Do toDo)
        {
            await _repo.AddAsync(cancellationToken, toDo);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            return await _repo.AnyAsync(cancellationToken, x=>(x.Id == id));
        }
        public async Task<bool> Exists(CancellationToken cancellationToken, int id, int userId)
        {
            return await _repo.AnyAsync(cancellationToken, x => (x.Id == id && x.OwnerId==userId) );
        }
        public async Task<List<To_Do>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }

        public async Task<List<To_Do>> GetAllofUserAsync(CancellationToken cancellationToken, int userId)
        {
            // return await Task.FromResult(_repo.Table.Where(x => x.IsDeleted == false && x.OwnerId == userId).ToList());

            return await _repo.Table.Where(x => x.IsDeleted == false && x.OwnerId == userId)
                                    .Include(x => x.Subtasks).ToListAsync(cancellationToken);
        }

        public async Task<To_Do> GetAsync(CancellationToken cancellationToken, int id)
        {
            return await _repo.GetAsync(cancellationToken, id);
        }

        public async Task<To_Do> GetAsync(CancellationToken cancellationToken, int id, int userId)
        {
            return await Task.FromResult(_repo.Table.SingleOrDefault(x => x.OwnerId == userId && x.Id == id && x.IsDeleted==false));
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id)
        {
            await _repo.RemoveAsync(cancellationToken, id);
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, To_Do toDo)
        {
            await _repo.RemoveAsync(cancellationToken, toDo);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, To_Do toDo)
        {
            await _repo.UpdateAsync(cancellationToken, toDo);
        }

    }
}
