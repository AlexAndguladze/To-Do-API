using ToDo.Application;
using ToDo.Application.Subtasks;
using ToDo.Application.To_Dos;
using ToDo.Application.Users;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Subtasks;
using ToDo.Infrastructure.To_Dos;
using ToDo.Infrastructure.Users;

namespace ToDo.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IToDoService, ToDoService>();
            service.AddScoped<IToDoRepository, ToDoRepository>();

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserRepository, UserRepository>();

            service.AddScoped<ISubtaskService, SubtaskService>();
            service.AddScoped<ISubtaskRepository, SubtaskRepository>();

            service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
           
        }
    }
}
