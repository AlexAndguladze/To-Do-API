using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Subtasks;

namespace ToDo.Infrastructure.Subtasks
{
    public interface ISubtaskRepository
    {
        Task AddAsync(CancellationToken cancellationToken, Subtask subtask);
        Task RemoveAsync(CancellationToken cancellationToken, int id);
    }
}
