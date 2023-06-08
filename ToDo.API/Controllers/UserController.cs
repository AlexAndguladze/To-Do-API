using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDo.API.Infrastructure.Auth.JWT;
using ToDo.Application.Users;
using ToDo.Application.Users.Requests;
using ToDo.Application.Users.Responses;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IOptions<JWTConfiguration> _options;

        public UserController(IUserService service, IOptions<JWTConfiguration> options)
        {
            _service = service;
            _options = options;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<UserResponseModel> Register(CancellationToken cancellationToken, UserCreateModel user)
        {
            return await _service.AddAsync(cancellationToken, user);
        }

        [Route("LogIn")]
        [HttpPost]
        public async Task<string> LogIn(CancellationToken cancellation, UserLogInRequest request)
        {
            var result = await _service.AuthenticateAsync(cancellation, request.Username, request.Password);

            return JWTHelper.GenerateSecurityToken(result, _options);
        }
    }
}
