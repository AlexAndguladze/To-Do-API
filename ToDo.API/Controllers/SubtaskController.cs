using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Subtasks;
using ToDo.Application.Subtasks.requests;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubtaskController : ControllerBase
    {
        private readonly ISubtaskService _service;

        public SubtaskController(ISubtaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task Create(CancellationToken cancellationToken, SubtaskRequestModel request, int toDoId)
        {
            await _service.AddAsync(cancellationToken, request, toDoId);
        }
        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _service.RemoveAsync(cancellationToken, id);
        }
    }
}
