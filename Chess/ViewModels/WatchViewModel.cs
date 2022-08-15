using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestChess.HelpElems;

namespace WPFTestChess.ViewModels
{
    internal class WatchViewModel
    {
        public User MyAccaunt
        {
            get
            {
                return MyClient.User;
            }
        }
    }
}
