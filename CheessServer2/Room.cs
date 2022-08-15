using Db;
using Library.Boards;
using Library.Boards.BoardNS;
using Library.GameElems;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    internal class Room
    {
        public List<Client> Clients = new List<Client>();

        ServerChessboard chessboard = new ServerChessboard();
        Server server;
        public Room(Server server)
        {
            this.server = server;
        }
        internal void AddClient(Client client)
        {
            if (Clients.Count < 2)
            {
                Clients.Add(client);
                client.ChatConnection(this);

                GameConect();
            }
        }

        internal void Close()
        {
            for(int i=0; i<Clients.Count; i++)
                Clients[i].Close();
        }
        internal void Stop()
        {
            //tcpListener.Stop();
        }

        public void WhiteWin()
        {
            User u1 = server.AddPoints(Clients[0].User);
            User u2 = server.RemovePoints(Clients[1].User);

            SendUser(u1, 0);
            SendUser(u2, 1);

            Clients[0].RemoveRoom();
            Clients[1].RemoveRoom();

            server.RemoveRoom(this);
        }
        public void BlackWin()
        {
            User u1 = server.RemovePoints(Clients[0].User);
            User u2 = server.AddPoints(Clients[1].User);

            SendUser(u1, 0);
            SendUser(u2, 1);

            Clients[0].RemoveRoom();
            Clients[1].RemoveRoom();

            server.RemoveRoom(this);
        }

        internal void Step(Step step, string id)
        {
            ResaltMsg msg = chessboard.MovePiece(step);
            if (msg.GameState == GameState.WhiteWin || msg.GameState == GameState.BlackWin)
            {
                if (msg.GameState == GameState.WhiteWin) WhiteWin();
                else BlackWin();
            }
            else
            {
                if (msg.CanMove)
                    BroadcastMessage(msg);
                else SendOneMsg(msg, id);
            }
        }
        internal void Roque(RoqueMsg roqueMsg, string id)
        {
            ResaltMsg msg = chessboard.Roque(roqueMsg.Step);

            if (msg.CanMove)
                BroadcastMessage(msg);
            else SendOneMsg(msg, id);
        }
        internal void GameConect()
        {
            PieceColors color = Clients.Count == 1 ? PieceColors.White : PieceColors.Black;

            string data = Message.Serialize(Message.FromValue(new GameColor() { Color = color }));
            byte[] data2 = Encoding.Unicode.GetBytes(data);
            Clients.Last().Stream.Write(data2, 0, data2.Length);
        }

        protected internal void BroadcastMessage(ResaltMsg message)
        {
            string data2 = Message.Serialize(Message.FromValue(message));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            if (Clients.Count == 2)
            {
                Clients[0].Stream.Write(data, 0, data.Length);
                Clients[1].Stream.Write(data, 0, data.Length);
            }
        }
        protected internal void SendOneMsg(ResaltMsg msg, string id)
        {
            string data2 = Message.Serialize(Message.FromValue(msg));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            if (Clients.Count == 2)
            {
                if (Clients[0].Id == id) Clients[0].Stream.Write(data, 0, data.Length);
                else Clients[1].Stream.Write(data, 0, data.Length);
            }
        }
        protected void SendUser(User user, int i)
        {
            string data2 = Message.Serialize(Message.FromValue(user));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            Clients[i].User = user;
            Clients[i].Stream.Write(data, 0, data.Length);
            //SendRating(user.Rating, i);
        }
        protected void SendRating(Rating r, int i)
        {
            string data2 = Message.Serialize(Message.FromValue(r));
            byte[] data = Encoding.Unicode.GetBytes(data2);
            Clients[i].Stream.Write(data, 0, data.Length);
        }
    }
}
