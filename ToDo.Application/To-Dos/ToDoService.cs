using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.To_Dos.Requests;
using ToDo.Application.To_Dos.Responses;
using Mapster;
using ToDo.Domain.To_Dos;
using ToDo.Application.CustomExceptions;
using ToDo.Domain.Statuses;

namespace ToDo.Application.To_Dos
{
    public class ToDoService : IToDoService
    {

        private readonly IToDoRepository _repo;

        public ToDoService(IToDoRepository repo)
        {
            _repo = repo;
        }
        public async Task AddAsync(CancellationToken cancellationToken, ToDoRequestModel toDo, int userId)
        {
            var toInsert = toDo.Adapt<To_Do>();
            toInsert.OwnerId = userId;
            await _repo.AddAsync(cancellationToken, toInsert);
        }

        public Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ToDoResponseModel>> GetAllofUserAsync(CancellationToken cancellationToken, int userId)
        {
            var toReturn = await _repo.GetAllofUserAsync(cancellationToken, userId);
            return toReturn.Adapt<List<ToDoResponseModel>>();
        }

        public async Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id)
        {
            var toReturn = await _repo.GetAsync(cancellationToken, id);
            return toReturn.Adapt<ToDoResponseModel>();
        }

        public async Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId)
        {
           var toReturn = await _repo.GetAsync(cancellationToken, id, userId);
            if(toReturn == null)
            {
                throw new ItemNotFoundException("Item does not exist");
            }
            return toReturn.Adapt<ToDoResponseModel>();
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if(await _repo.Exists(cancellationToken, id, userId)==false)
            {
                throw new ItemNotFoundException("Item does not exist");
            }
            await _repo.RemoveAsync(cancellationToken, id);
        }

        public Task RemoveAsync(CancellationToken cancellationToken, ToDoRequestModel toDo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(CancellationToken cancellationToken, int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, ToDoRequestModel toDo, int id, int userId)
        {
            var exists =await _repo.Exists(cancellationToken, id, userId);
            if(exists == false)
            {
                throw new ItemNotFoundException("Item does not exist");
            }
            var toUpdate = toDo.Adapt<To_Do>();
            toUpdate.Id = id;
            toUpdate.OwnerId= userId;
            await _repo.UpdateAsync(cancellationToken, toUpdate);
        }

        public async Task UpdateStatusAsync(CancellationToken cancellationToken, Status status, int id, int userId)
        {
            var exists = await _repo.Exists(cancellationToken, id, userId);
            if (exists == false)
            {
                throw new ItemNotFoundException("Item does not exist");
            }
            var toChange = await _repo.GetAsync(cancellationToken, id, userId);
            toChange.Status = status;
            await _repo.UpdateAsync(cancellationToken, toChange);
        }
    }
}
