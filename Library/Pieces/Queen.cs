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
    public class Queen : Piece
    {
        public Queen(PieceColors color, PieceType type, string texturePath, PointInt boardPointInt) :
            base(color, type, texturePath, boardPointInt)
        { }

        bool flag;
        public override void Move(Piece[,] board)
        {
            movePointInts.Clear();
            attackPointInts.Clear();

            AddNumbWithFor(board,1, 1);
            AddNumbWithFor(board,- 1, 1);
            AddNumbWithFor(board, 1, -1);
            AddNumbWithFor(board, -1, -1);

            AddNumbWithFor(board, 0, 1);
            AddNumbWithFor(board, 0, -1);
            AddNumbWithFor(board, 1, 0);
            AddNumbWithFor(board, -1, 0);
        }
        public void AddNumbWithFor(Piece[,] board,int xMng, int yMng)
        {
            for (int i = 1;i>=0 ; i++)
            {
                flag = false;
                AddNumb(board,(int)ChessboardPointInt.X + i * xMng, (int)ChessboardPointInt.Y + i * yMng);
                if (flag) break;
            }
        }
        public void AddNumb(Piece[,] board,int x, int y)
        {
            if (x >= 0 && x < Chessboard.CELL_COUNT && y >= 0 && y < Chessboard.CELL_COUNT)
            {
                if (board[y, x] != null)
                {
                    if (board[y, x].Color != Color) attackPointInts.Add(new PointInt(x, y));
                    flag = true;
                }
                else movePointInts.Add(new PointInt(x, y));
            }
            else flag = true;
        }
    }
}
