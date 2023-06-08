using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Subtasks.requests;
using ToDo.Domain.Subtasks;
using ToDo.Infrastructure.Subtasks;

namespace ToDo.Application.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        readonly ISubtaskRepository _repo;

        public SubtaskService(ISubtaskRepository repo)
        {
            _repo = repo;
        }
        public async Task AddAsync(CancellationToken cancellationToken, SubtaskRequestModel model, int todoId)
        {
            var toAdd = model.Adapt<Subtask>();
            toAdd.ToDoItemId = todoId;
            await _repo.AddAsync(cancellationToken, toAdd);
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id)
        {
            await _repo.RemoveAsync(cancellationToken, id);
        }
    }
}
