using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.To_Dos;

namespace ToDo.Application.To_Dos
{
    public interface IToDoRepository
    {
        Task<List<To_Do> >GetAllAsync(CancellationToken cancellationToken);
        Task<To_Do> GetAsync(CancellationToken cancellationToken, int id);
        Task<To_Do> GetAsync(CancellationToken cancellationToken, int id, int userId);
        Task AddAsync(CancellationToken cancellationToken, To_Do toDo);
        Task RemoveAsync(CancellationToken cancellationToken, int id);
        Task RemoveAsync(CancellationToken cancellationToken, To_Do toDo);
        Task UpdateAsync(CancellationToken cancellationToken, To_Do toDo);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id, int userId);
        Task<List<To_Do>> GetAllofUserAsync(CancellationToken cancellationToken,int userId);
    }
}
