using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.HelpElems;

namespace Library.GameElems
{
    public class SignUpMsg : MyNotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public SignUpMsg(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
