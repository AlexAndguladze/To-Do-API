using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.To_Dos;
using ToDo.Application.To_Dos.Requests;
using ToDo.Application.To_Dos.Responses;
using ToDo.Domain.Statuses;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Gets ID of authorzed user
        /// </summary>
        [HttpGet("get-my-id")]
        public int GetUserId()
        {
            var userId =User.Claims.FirstOrDefault(x => x.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
            int count = 0;
            if (userId != null)
            {
                return Int32.Parse(userId.Value);
            }
            return 0;
        }

        [HttpPost()]
        public async Task Create(CancellationToken cancellationToken, ToDoRequestModel request)
        {
            int userId = GetUserId();
            await _service.AddAsync(cancellationToken, request, userId);
        }
        /// <summary>
        /// Updates to-do status: 0 - Active, 1 - Done
        /// </summary>
        [HttpPost("{id}/{status}")]
        public async Task UpdateStatus(CancellationToken cancellationToken, int id, Status status)
        {
            int userId = GetUserId();
            await _service.UpdateStatusAsync(cancellationToken, status ,id, userId);
        }

        [HttpGet("{id}")]
        public async Task<ToDoResponseModel> Get(CancellationToken cancellationToken, int id)
        {
            int userId = GetUserId();
            return await _service.GetAsync(cancellationToken, id, userId);
        }
        /// <summary>
        /// Returns all To-Dos of authorzed user
        /// </summary>
        [HttpGet()]
        public async Task<List<ToDoResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            int userId = GetUserId();
            return await _service.GetAllofUserAsync(cancellationToken, userId);
        }
        [HttpPut("{id}")]
        public async Task Update (CancellationToken cancellationToken,ToDoRequestModel request, int id)
        {
            int userId = GetUserId();
            await _service.UpdateAsync(cancellationToken, request, id, userId);
        }
        [HttpDelete("{id}")]
        public async Task Delete (CancellationToken cancellationToken,int id)
        {
            int userId = GetUserId();
            await _service.RemoveAsync(cancellationToken, id, userId);
        }
        
    }
}
