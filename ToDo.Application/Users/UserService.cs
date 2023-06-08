using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.CustomExceptions;
using ToDo.Application.Users.Requests;
using ToDo.Application.Users.Responses;
using ToDo.Domain.Users;

namespace ToDo.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly string SECRET_KEY = "HaShaSaHa";
        public UserService(IUserRepository repo)
        {
            _repo=repo;
        }
        public async Task<UserResponseModel> AddAsync(CancellationToken cancellationToken, UserCreateModel userModel)
        {
            var exists = await _repo.Exists(cancellationToken, userModel.Username);

            if (exists)
            {
                throw new Exception("user already exists");
            }
            var hashedPassword = GenerateHash(userModel.Password);
            var userEntity = userModel.Adapt<User>();
            userEntity.PasswordHash = hashedPassword;
            var exisedUser = await _repo.AddAsync(cancellationToken, userEntity);

            return exisedUser.Adapt<UserResponseModel>();
        }

        public async Task<string> AuthenticateAsync(CancellationToken cancellationToken, string username, string password)
        {
            var hashedPassword = GenerateHash(password);
            var userEntity = await _repo.GetAsync(cancellationToken, username, hashedPassword);
            if(userEntity == null)
            {
                throw new IncorrectUsernameOrPasswordException("User or Password is incorrect");
            }
            return userEntity.Id.ToString();
        }
        private string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
