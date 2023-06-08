using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.Subtasks;

namespace ToDo.Infrastructure.Subtasks
{
    public class SubtaskRepository:ISubtaskRepository
    {
        private IBaseRepository<Subtask> _repo;

        public SubtaskRepository(IBaseRepository<Subtask> repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(CancellationToken cancellationToken, Subtask subtask)
        {
            await _repo.AddAsync(cancellationToken, subtask);
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id)
        {
            await _repo.RemoveAsync(cancellationToken, id);
        }
    }
}
