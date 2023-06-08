using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Users.Requests;
using ToDo.Application.Users.Responses;

namespace ToDo.Application.Users
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(CancellationToken cancellationToken, string username, string password);
        Task <UserResponseModel> AddAsync(CancellationToken cancellationToken, UserCreateModel user);
    }
}
