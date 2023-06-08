using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Subtasks.requests;

namespace ToDo.Application.Subtasks
{
    public interface ISubtaskService
    {
        Task AddAsync (CancellationToken cancellationToken, SubtaskRequestModel model ,int todoId);
        Task RemoveAsync (CancellationToken cancellationToken, int id);
    }
}
