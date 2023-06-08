using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Users.Requests
{
    public class UserLogInRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
