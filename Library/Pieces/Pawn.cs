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
    public class Pawn : Piece
    {
        public Pawn(PieceColors color, PieceType type, string texturePath, PointInt boardPointInt) :
            base(color, type, texturePath, boardPointInt)
        { }

        public bool isFirstMove = false;
        public override void Move(Piece[,] board)
        {
            movePointInts.Clear();
            attackPointInts.Clear();

            int moveVectorY = Color == PieceColors.White ? 1 : -1;

            if (isFirstMove)
            {
                if (ChessboardPointInt.Y + moveVectorY * 2 >= 0 &&
                   ChessboardPointInt.Y + moveVectorY * 2 < Chessboard.CELL_COUNT)
                {
                    if (board[(int)(ChessboardPointInt.Y + moveVectorY * 2), (int)ChessboardPointInt.X] == null &&
                        board[(int)(ChessboardPointInt.Y + moveVectorY), (int)ChessboardPointInt.X] == null)
                        movePointInts.Add(new PointInt((int)ChessboardPointInt.X, (int)(ChessboardPointInt.Y + moveVectorY * 2)));
                }
            }
            if (ChessboardPointInt.Y + moveVectorY >= 0 && ChessboardPointInt.Y + moveVectorY < Chessboard.CELL_COUNT)
                if (board[(int)(ChessboardPointInt.Y + moveVectorY), (int)ChessboardPointInt.X] == null)
                    movePointInts.Add(new PointInt((int)ChessboardPointInt.X, (int)(ChessboardPointInt.Y + moveVectorY)));

            int xP, yP;

            xP = (int)ChessboardPointInt.X-1;
            yP = (int)(ChessboardPointInt.Y + moveVectorY);

            if (xP >= 0 && xP < Chessboard.CELL_COUNT && yP >= 0 && yP < Chessboard.CELL_COUNT)
                if (board[yP, xP] != null)
                    if (board[yP, xP].Color != Color) attackPointInts.Add(new PointInt(xP, yP));

            if (xP + 2 >= 0 && xP + 2 < Chessboard.CELL_COUNT && yP >= 0 && yP < Chessboard.CELL_COUNT)
                if (board[yP, xP + 2] != null)
                    if (board[yP, xP + 2].Color != Color) attackPointInts.Add(new PointInt(xP + 2, yP));
        }
    }
}
