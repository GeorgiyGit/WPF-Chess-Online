using Library.Boards;
using Library.Boards.BoardNS;
using Library.GameElems;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFTestChess.HelpElems;

namespace WPFTestChess.PlayPage.Games
{
    internal class ChessboardWithBot:ChessboardViewModel
    {
        Bot bot;
        BotBoard chessboard; 
        
        public ChessboardWithBot(PieceColors color)
        {
            OurColor = color;

            //chessboard = new BotBoard(MouseClick, IsMouseCLick, GameOver);
            if (color == PieceColors.White) Chessboard.IsWhiteButton = true;
            //backCellsList = Converter.TwoDemensionalArrayToCollection(chessboard.board.board);

            //chessboard.board.ClearPress();

            click = new RelayCommand(MouseClick, null);


            startNewGameCmd = new RelayCommand((t) => StartNewGame(), null);


            bot = new Bot();
            bot.Color = color == PieceColors.White ? PieceColors.Black : PieceColors.White;
        }

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
    }
}
