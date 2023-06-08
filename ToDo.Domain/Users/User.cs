using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Abstractions;
using ToDo.Domain.To_Dos;

namespace ToDo.Domain.Users
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<To_Do> ToDos { get; set; }
    }
}
