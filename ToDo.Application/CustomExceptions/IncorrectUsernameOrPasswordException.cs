using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.CustomExceptions
{
    public class IncorrectUsernameOrPasswordException:Exception
    {
        public string code = "IncorrectUsernameOrPasswordException";

        public IncorrectUsernameOrPasswordException(string message) : base(message)
        {

        }
    }
}
