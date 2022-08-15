using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.GameElems
{
    public class LogInMsg
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public LogInMsg(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
