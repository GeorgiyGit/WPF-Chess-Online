using Library.Boards;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Pieces
{
    public class King : Piece
    {
        public King(PieceColors color, PieceType type, string texturePath, PointInt boardPointInt) :
            base(color, type, texturePath, boardPointInt)
        { }

        public override void Move(Piece[,] board)
        {
            movePointInts.Clear();
            attackPointInts.Clear();

            AddNumbWithPlusPointInt(board, -1, +1);
            AddNumbWithPlusPointInt(board, 0, +1);
            AddNumbWithPlusPointInt(board, +1, +1);

            AddNumbWithPlusPointInt(board, -1, 0);
            AddNumbWithPlusPointInt(board, +1, 0);

            AddNumbWithPlusPointInt(board, -1, -1);
            AddNumbWithPlusPointInt(board, 0, -1);
            AddNumbWithPlusPointInt(board, +1, -1);
        }
        public bool isFirstMove = true;
        public void AddNumbWithPlusPointInt(Piece[,] board,int x,int y)
        {
            AddNumb(board,(int)ChessboardPointInt.X + x, (int)ChessboardPointInt.Y + y);
        }
        public void AddNumb(Piece[,] board,int x, int y)
        {
            if (x >= 0 && x < Chessboard.CELL_COUNT && y >= 0 && y < Chessboard.CELL_COUNT)
            {
                if (board[y, x] != null)
                {
                    if (board[y, x].Color != Color) attackPointInts.Add(new PointInt(x, y));
                }
                else movePointInts.Add(new PointInt(x, y));
            }
        }
    }
}
