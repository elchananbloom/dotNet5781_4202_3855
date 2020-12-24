using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserDAO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ManegementAccess { get; set; }

        public bool Deleted { get; set; }
    }
}
