using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Statuses;

namespace ToDo.Application.To_Dos.Requests
{
    public class ToDoRequestModel
    {
        public string Title { get; set; }
        //public Status Status { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
