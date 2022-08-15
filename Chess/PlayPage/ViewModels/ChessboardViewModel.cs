using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.HelpElems;
using Library.Boards;
using Library.Boards.BoardNS;
using Library.GameElems;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using WPFTestChess.HelpElems;
using RelayCommand = WPFTestChess.HelpElems.RelayCommand;
using WPFTestChess.PlayPage.Games;
using Db;
using Library.Pieces;

namespace WPFTestChess.PlayPage.ViewModels
{
    internal class OnlineChessboardViewModel :ChessboardViewModel
    {

        public Chessboard chessboard { get; set; }
        public OnlineChessboardViewModel(GameColor start, NavigateDel clickD)
        {
            navigate = clickD;
            OurColor = start.Color;

            chessboard = new Chessboard(MouseClick, IsMouseCLick, GameOver);
            if (start.Color == PieceColors.White) Chessboard.IsWhiteButton = true;
            backCellsList = Converter.TwoDemensionalArrayToCollection(chessboard.board.board);

            chessboard.board.ClearPress();

            click = new RelayCommand(MouseClick, null);

            startNewGameCmd = new RelayCommand((t) => StartNewGame(), null);

            if (start.Color == PieceColors.Black)
            {
                Thread receiveThread = new Thread(new ThreadStart(GetStep));
                receiveThread.Start();
            }
        }

        ~OnlineChessboardViewModel()
        {
            PlayerDisconected p = new PlayerDisconected(MyClient.User);
            string data = Message.Serialize(Message.FromValue(p));
            try
            {
                byte[] data2 = Encoding.Unicode.GetBytes(data);
                MyClient.Stream.Write(data2, 0, data2.Length);


                Thread receiveThread = new Thread(new ThreadStart(GetStep));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private ObservableCollection<BoardCell> backCellsList = new ObservableCollection<BoardCell>();
        public IEnumerable<BoardCell> BackCells => backCellsList;

        public BoardCell selectedBC;

        private bool isCanSelected = true;
        public void MouseClick(object obj)
        {
            BoardCell bc = obj as BoardCell;


            if (Chessboard.SelectedColor == OurColor)
            {
                if (bc.Piece != null && isCanSelected)
                {
                    if (bc.Piece.Color == OurColor)
                    {
                        BcChange(bc);
                    }
                }
                else if (bc.Piece == null || bc.Piece.Color != OurColor)
                {
                    Step step = new Step(selectedBC.Piece.Type, selectedBC.Piece.Color, selectedBC.Point, bc.Point);
                    string data = Message.Serialize(Message.FromValue(step));
                    try
                    {
                        byte[] data2 = Encoding.Unicode.GetBytes(data);
                        MyClient.Stream.Write(data2, 0, data2.Length);


                        Thread receiveThread = new Thread(new ThreadStart(GetStep));
                        receiveThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    //chessboard.MovePiece(ref selectedBC, ref bc);
                }
                else if (selectedBC.Piece != null && selectedBC.Piece.Type == PieceType.King && bc.Piece != null && bc.Piece.Type == PieceType.Rook)
                {
                    RoqueMsg msg = new RoqueMsg(new Step(PieceType.King, OurColor, selectedBC.Point, bc.Point));
                    string data = Message.Serialize(Message.FromValue(msg));
                    try
                    {
                        byte[] data2 = Encoding.Unicode.GetBytes(data);
                        MyClient.Stream.Write(data2, 0, data2.Length);


                        Thread receiveThread = new Thread(new ThreadStart(GetStep));
                        receiveThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    BcChange(bc);
                }
            }
        }

        private void BcChange(BoardCell bc)
        {
            isCanSelected = false;
            selectedBC = bc;
            selectedBC.Piece.Move(Converter.BoardCellsToPieces(ref chessboard.board.board));

            chessboard.board.ClearCircle();
            chessboard.board.ClearPress();

            foreach (var p in selectedBC.Piece.movePointInts)
            {
                chessboard.board.board[p.Y, p.X].BackCell.IsCircle = true;
                chessboard.board.board[p.Y, p.X].BackCell.IsAttackCircle = false;
            }

            foreach (var p in selectedBC.Piece.attackPointInts)
            {
                chessboard.board.board[p.Y, p.X].BackCell.IsCircle = true;
                chessboard.board.board[p.Y, p.X].BackCell.IsAttackCircle = true;
            }

            selectedBC.BackCell.IsPressedTrue = true;
        }

        private void GetStep()
        {
            byte[] data = new byte[128];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = MyClient.Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (MyClient.Stream.DataAvailable);

            Message message = Message.Deserialize(builder.ToString());
            if (message.Type == typeof(ResaltMsg))
            {
                ResaltMsg resaltMsg = message.Value.ToObject<ResaltMsg>();

                switch (resaltMsg.Type)
                {
                    case ResaltType.AttackMove:
                        if (resaltMsg.CanMove)
                        {
                            if(chessboard.board.board[resaltMsg.Step.CurrentPoint.Y,resaltMsg.Step.CurrentPoint.X].Piece.Type==PieceType.Pawn)
                            {
                                (chessboard.board.board[resaltMsg.Step.CurrentPoint.Y, resaltMsg.Step.CurrentPoint.X].Piece as Pawn).isFirstMove = false;
                            }
                            chessboard.board.ClearCircle();

                            isCanSelected = true;
                            chessboard.board.ReplacePiece(resaltMsg.Step);

                            chessboard.board.board[resaltMsg.Step.NextPoint.Y, resaltMsg.Step.NextPoint.X].Piece.Move(Converter.BoardCellsToPieces(ref chessboard.board.board));

                            Chessboard.ChangeColor();
                            chessboard.board.gameState = resaltMsg.GameState;

                            if (Chessboard.SelectedColor != OurColor)
                            {
                                GetStep();
                            }

                            selectedBC = null;
                        }
                        //else GetStep();
                        break;
                    case ResaltType.Roque:
                        if (resaltMsg.CanMove)
                        {
                            if (chessboard.board.board[resaltMsg.Step.CurrentPoint.Y, resaltMsg.Step.CurrentPoint.X].Piece.Type == PieceType.Pawn)
                            {
                                (chessboard.board.board[resaltMsg.Step.CurrentPoint.Y, resaltMsg.Step.CurrentPoint.X].Piece as Pawn).isFirstMove = false;
                            }

                            chessboard.board.ClearCircle();
                            isCanSelected = true;

                            chessboard.board.RoqueMove(resaltMsg.Step);

                            Chessboard.ChangeColor();
                            chessboard.board.gameState = resaltMsg.GameState;

                            if (Chessboard.SelectedColor != OurColor)
                            {
                                GetStep();
                            }

                            selectedBC = null;
                        }
                        break;
                }
                
            }
            else if(message.Type==typeof(User))
            {
                MyClient.User = message.Value.ToObject<User>();

                //GetStep();

                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() =>
                     {
                         navigate.Invoke(new Uri(@"HomeWindow\HomeWindow.xaml", UriKind.Relative));
                     }));
            }
            else if (message.Type == typeof(Rating))
            {
                Rating r = message.Value.ToObject<Rating>();

                MyClient.User.Rating = r;
            }
        }

        static void Disconnect()
        {
            if (MyClient.Stream != null)
                MyClient.Stream.Close();
            if (MyClient.Stream != null)
                MyClient.Stream.Close();
            Environment.Exit(0);
        }


        NavigateDel navigate;
        public delegate bool NavigateDel(Uri obj);
    }
}
