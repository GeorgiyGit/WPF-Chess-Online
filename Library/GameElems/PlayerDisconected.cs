using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.GameElems
{
    public class PlayerDisconected
    {
        public User User { get; set; }
        public PlayerDisconected(User user)
        {
            User = user;
        }
    }
}
