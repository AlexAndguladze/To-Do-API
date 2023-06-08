using Mapster;
using ToDo.Application.Subtasks.requests;
using ToDo.Application.Subtasks.Responses;
using ToDo.Application.To_Dos.Requests;
using ToDo.Application.To_Dos.Responses;
using ToDo.Application.Users.Requests;
using ToDo.Domain.Subtasks;
using ToDo.Domain.To_Dos;
using ToDo.Domain.Users;

namespace ToDo.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            TypeAdapterConfig<ToDoRequestModel, To_Do>.NewConfig().TwoWays();
            TypeAdapterConfig<ToDoResponseModel, To_Do>.NewConfig().TwoWays();

            //TypeAdapterConfig<User, UserCreateModel>.NewConfig().Map(u => u.Password, m => m.PasswordHash).TwoWays();
            TypeAdapterConfig<UserCreateModel, User>.NewConfig().TwoWays();
            TypeAdapterConfig<SubtaskResponseModel, Subtask>.NewConfig().TwoWays();
            TypeAdapterConfig<SubtaskRequestModel, Subtask>.NewConfig().TwoWays();
        }
    }
}
