using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Users;

namespace ToDo.Application.Users
{
    public interface IUserRepository
    {
        Task<User> AddAsync(CancellationToken cancellationToken, User user);

        Task<User> GetAsync(CancellationToken cancellationToken, string username, string password);

        Task<bool> Exists(CancellationToken cancellationToken, string username);

        //Task<User> GetByTokenAsync(CancellationToken cancellationToken, string token);
    }
}
