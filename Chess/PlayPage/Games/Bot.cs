using Library.Boards;
using Library.Boards.BoardNS;
using Library.GameElems;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestChess.PlayPage.Games
{
    internal class Bot
    {
        private List<Step> stepsOne = new List<Step>();
        private List<Step> stepsTwo = new List<Step>();
        public PieceColors Color { get; set; }

        private Random random = new Random();
        public Step GetStep(BoardCell[,] board)
        {
            StepCalculation(board);
            if (stepsTwo.Count > 0) return stepsTwo[random.Next(stepsTwo.Count)];
            return stepsOne[random.Next(stepsOne.Count)];
        }

        private void StepCalculation(BoardCell[,] board)
        {
            stepsOne.Clear();
            stepsTwo.Clear();
            for(int y=0;y<Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    if (board[y, x].Piece != null)
                    {
                        board[y, x].Piece.Move(Converter.BoardCellsToPieces(ref board));

                        foreach (var p in board[y, x].Piece.movePointInts)
                        {
                            stepsOne.Add(new Step(board[y, x].Piece.Type, board[y, x].Piece.Color, new PointInt(x, y), p));
                        }

                        foreach (var p in board[y, x].Piece.attackPointInts)
                        {
                            stepsTwo.Add(new Step(board[y, x].Piece.Type, board[y, x].Piece.Color, new PointInt(x, y), p));
                        }
                    }
                }
            }
        }
    }
}
