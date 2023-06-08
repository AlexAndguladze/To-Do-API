using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Application.Users;
using ToDo.Domain.Users;

namespace ToDo.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private IBaseRepository<User> _repo;

        public UserRepository(IBaseRepository<User> repo)
        {
            _repo = repo;
        }

        public async Task<User> AddAsync(CancellationToken cancellationToken, User user)
        {
            await _repo.AddAsync(cancellationToken, user);
            return user;
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, string username)
        {
            return await _repo.AnyAsync(cancellationToken, x=>x.Username == username);
        }

        public async Task<User> GetAsync(CancellationToken cancellationToken, string username, string password)
        {
            return await Task.FromResult(_repo.Table.FirstOrDefault(x=>x.Username == username && x.PasswordHash == password));
        }
    }
}
