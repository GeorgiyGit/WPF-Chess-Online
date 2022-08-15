using Library.GameElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace ChessServer
{
    internal class Client
    {
        public User User { get; set; }
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        Room room;
        Server server;
        public Client(TcpClient tcpClient, Server server)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            //server.AddConnection(this);
            this.server = server;
        }

        internal void ChatConnection(Room chat)
        {
            this.room = chat;
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();

                while (true)
                {
                    try
                    {
                        GetMessage();
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //chat.RemoveConnection(this.Id);
                Close();
            }
        }

        private void GetMessage()
        {

            byte[] data = new byte[128];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            Message message = Message.Deserialize(builder.ToString());
            if (message.Type == typeof(Step))
            {
                Step step = message.Value.ToObject<Step>();
                room?.Step(step, Id);
            }
            else if (message.Type == typeof(GameStart))
            {
                GameStart gameStart = message.Value.ToObject<GameStart>();
                //Id = gameStart.PlayerId;
                server.AddConnection(this);
            }
            else if(message.Type == typeof(RoqueMsg))
            {
                RoqueMsg roque = message.Value.ToObject<RoqueMsg>();
                room?.Roque(roque, Id);
            }
            else if (message.Type == typeof(SignUpMsg))
            {
                SignUpMsg signUp = message.Value.ToObject<SignUpMsg>();
                User u = server.SignUp(signUp);
                if (u != null)
                {
                    User = u;
                    SendUser(u);
                    //SendRating(u.Rating);
                }
                else
                {
                    SignLogError signLogError = new SignLogError(@"SignUpError");
                    SendError(signLogError);
                }
            }
            else if (message.Type == typeof(LogInMsg))
            {
                LogInMsg logInMsg = message.Value.ToObject<LogInMsg>();
                User u = server.LogIn(logInMsg);
                if (u != null)
                {
                    User = u;
                    SendUser(u);
                    //SendRating(u.Rating);
                }
                else
                {
                    SignLogError signLogError = new SignLogError(@"LogInError");
                    SendError(signLogError);
                }
            }
            else if (message.Type == typeof(PlayerDisconected))
            {
                PlayerDisconected plDis = message.Value.ToObject<PlayerDisconected>();

                if (room.Clients[0].Id == Id) room.BlackWin();
                else room.WhiteWin();
            }
            //else if(message.Type==typeof())

        }
        private void SendError(SignLogError error)
        {
            string data2 = Message.Serialize(Message.FromValue(error));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            Stream.Write(data, 0, data.Length);
        }
        private void SendUser(User user)
        {
            string data2 = Message.Serialize(Message.FromValue(user));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            Stream.Write(data, 0, data.Length);

        }
        protected void SendRating(Rating r)
        {
            string data2 = Message.Serialize(Message.FromValue(r));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            Stream.Write(data, 0, data.Length);
        }
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }

        public void RemoveRoom()
        {
            room = null;
        }
    }
}
