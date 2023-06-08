using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.To_Dos.Requests;
using ToDo.Application.To_Dos.Responses;
using ToDo.Domain.Statuses;

namespace ToDo.Application.To_Dos
{
    public interface IToDoService
    {
        Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id);
        Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId);
        Task AddAsync(CancellationToken cancellationToken, ToDoRequestModel toDo, int userId);
        Task RemoveAsync(CancellationToken cancellationToken, int id);
        Task RemoveAsync(CancellationToken cancellationToken, int id, int userId);
        Task RemoveAsync(CancellationToken cancellationToken, ToDoRequestModel toDo);
        Task UpdateAsync(CancellationToken cancellationToken, ToDoRequestModel toDo, int id, int userId);
        Task UpdateStatusAsync(CancellationToken cancellationToken,  Status status, int id, int userId);
        Task<List<ToDoResponseModel>> GetAllofUserAsync(CancellationToken cancellationToken, int id);
    }
}
