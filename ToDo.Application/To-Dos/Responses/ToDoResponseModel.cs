using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Subtasks.Responses;
using ToDo.Domain.Statuses;
using ToDo.Domain.Subtasks;

namespace ToDo.Application.To_Dos.Responses
{
    public class ToDoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public String Status { get; set; }
        public DateTime? CompletionDate { get; set; }
        public List<SubtaskResponseModel> Subtasks { get; set; }
    }
}
