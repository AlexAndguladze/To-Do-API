using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Statuses;
using ToDo.Domain.Subtasks;
using ToDo.Domain.Users;

namespace ToDo.Domain.To_Dos
{
    public class To_Do:BaseEntity
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int OwnerId { get; set; }
        public virtual User User { get; set; }
        public List<Subtask> Subtasks { get; set; }
    }
}
