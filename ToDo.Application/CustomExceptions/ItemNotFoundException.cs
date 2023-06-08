using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.CustomExceptions
{
    public class ItemNotFoundException:Exception
    {
        public string code = "ItemNotFoundException";
        public ItemNotFoundException(string message) : base(message)
        {

        }
    }
}
