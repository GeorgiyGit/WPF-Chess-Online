using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestChess.HelpElems
{
    internal class MyClient
    {

        private const string host = "127.0.0.1";
        private const int port = 8888;

        public static TcpClient Client { get; set; }
        public static NetworkStream Stream { get; set; }

        public static User User{get;set;}

        public static void Connect()
        {
            Client = new TcpClient();
            Client.Connect(host, port);
            Stream = Client.GetStream();
        }
    }
}
