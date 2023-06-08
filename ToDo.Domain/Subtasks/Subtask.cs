using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Abstractions;
using ToDo.Domain.To_Dos;

namespace ToDo.Domain.Subtasks
{
    public class Subtask:BaseEntity
    {
        public string Title { get; set; }
        public int ToDoItemId { get; set; }

        public virtual To_Do ToDo { get; set; }
    }
}
