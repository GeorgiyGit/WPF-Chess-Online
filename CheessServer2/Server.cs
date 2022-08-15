using Db;
using Library.GameElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    internal class Server
    {
        static TcpListener tcpListener;
        List<Room> chats = new List<Room>();
        List<Client> clients = new List<Client>();

        ChessContext db;
        
        public Server()
        {
            db = new ChessContext();
        }

        public User SignUp(SignUpMsg msg)
        {
            if (Regex.IsMatch(msg.Name, @"^\S{3,25}$") &&
                Regex.IsMatch(msg.Email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?") &&
                Regex.IsMatch(msg.Password, @"^\S{8,}$"))
            {
                return db.AddUser(msg.Name, msg.Email, msg.Password);
            }
            return null;
        }

        public void RemoveRoom(Room room)
        {
            chats.Remove(room);
        }
        public User LogIn(LogInMsg msg)
        {
            if (Regex.IsMatch(msg.Name, @"^\S{3,25}$") &&
                Regex.IsMatch(msg.Password, @"^\S{8,}$"))
            {
                return db.LogIn(msg.Name, msg.Password);
            }
            return null;
        }

        public User? AddPoints(User user)
        {
            return db.AddRating(user);
        }
        public User? RemovePoints(User user)
        {
            return db.RemoveRating(user);
        }

        protected internal void AddConnection(Client client)
        {
            if (chats.Count > 0 && chats.Last().Clients.Count < 2) chats.Last().AddClient(client);
            else
            {
                chats.Add(new Room(this));
                chats.Last().AddClient(client);
            }

        }
        protected internal void RemoveConnection(string id)
        {
            Client client;
            foreach (var c in chats)
            {
                client = c.Clients.FirstOrDefault(t => t.Id == id);
                if (client != null) c.Clients.Remove(client);
            }
        }
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    Client clientObject = new Client(tcpClient, this);
                    clients.Add(clientObject);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void Disconnect()
        {

            tcpListener.Stop();

            for (int i = 0; i < chats.Count; i++)
            {
                chats[i].Close();
            }
            Environment.Exit(0);
        }
    }
}
