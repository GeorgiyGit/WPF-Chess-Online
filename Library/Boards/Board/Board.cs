using Library.Boards.AttackBoardNS;
using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Boards.BoardNS
{
    public class Board
    {
        public GameState gameState { get; set; }
        public void InitializePiece(ref Piece[,] initialBoard)
        {
            for (int i = 0; i < Chessboard.CELL_COUNT; i++)
            {
                initialBoard[1, i] = new Pawn(PieceColors.White, PieceType.Pawn, @"Resources\Pieces\White\Pawn.png", new PointInt(i, 1));
                initialBoard[6, i] = new Pawn(PieceColors.Black, PieceType.Pawn, @"Resources\Pieces\Black\Pawn.png", new PointInt(i, 6));

                (initialBoard[1, i] as Pawn).isFirstMove = true;
                (initialBoard[6, i] as Pawn).isFirstMove = true;
            }


            initialBoard[0, 0] = new Rook(PieceColors.White, PieceType.Rook, @"Resources\Pieces\White\Rook.png", new PointInt(0, 0));
            initialBoard[0, 7] = new Rook(PieceColors.White, PieceType.Rook, @"Resources\Pieces\White\Rook.png", new PointInt(7, 0));

            initialBoard[7, 0] = new Rook(PieceColors.Black, PieceType.Rook, @"Resources\Pieces\Black\Rook.png", new PointInt(0, 7));
            initialBoard[7, 7] = new Rook(PieceColors.Black, PieceType.Rook, @"Resources\Pieces\Black\Rook.png", new PointInt(7, 7));


            initialBoard[0, 1] = new Knight(PieceColors.White, PieceType.Knight, @"Resources\Pieces\White\Knight.png", new PointInt(1, 0));
            initialBoard[0, 6] = new Knight(PieceColors.White, PieceType.Knight, @"Resources\Pieces\White\Knight.png", new PointInt(6, 0));

            initialBoard[7, 1] = new Knight(PieceColors.Black, PieceType.Knight, @"Resources\Pieces\Black\Knight.png", new PointInt(1, 7));
            initialBoard[7, 6] = new Knight(PieceColors.Black, PieceType.Knight, @"Resources\Pieces\Black\Knight.png", new PointInt(6, 7));


            initialBoard[0, 2] = new Bishop(PieceColors.White, PieceType.Bishop, @"Resources\Pieces\White\Bishop.png", new PointInt(2, 0));
            initialBoard[0, 5] = new Bishop(PieceColors.White, PieceType.Bishop, @"Resources\Pieces\White\Bishop.png", new PointInt(5, 0));

            initialBoard[7, 2] = new Bishop(PieceColors.Black, PieceType.Bishop, @"Resources\Pieces\Black\Bishop.png", new PointInt(2, 7));
            initialBoard[7, 5] = new Bishop(PieceColors.Black, PieceType.Bishop, @"Resources\Pieces\Black\Bishop.png", new PointInt(5, 7));


            initialBoard[0, 3] = new Queen(PieceColors.White, PieceType.Queen, @"Resources\Pieces\White\Queen.png", new PointInt(3, 0));
            initialBoard[7, 3] = new Queen(PieceColors.Black, PieceType.Queen, @"Resources\Pieces\Black\Queen.png", new PointInt(3, 7));


            initialBoard[0, 4] = new King(PieceColors.White, PieceType.King, @"Resources\Pieces\White\King.png", new PointInt(4, 0));
            initialBoard[7, 4] = new King(PieceColors.Black, PieceType.King, @"Resources\Pieces\Black\King.png", new PointInt(4, 7));
        }


        public static List<PointInt> GetPiecesPoint(Piece[,] board, PieceType type, PieceColors color)
        {
            List<PointInt> points = new List<PointInt>();
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    if (board[y, x] != null)
                    {
                        if (board[y, x].Type == type && board[y, x].Color == color) points.Add(new PointInt(x, y));
                    }
                }
            }
            return points;
        }

        public static List<PointInt> GetPiecesPoint(BoardCell[,] board, PieceType type, PieceColors color)
        {
            return GetPiecesPoint(Converter.BoardCellsToPieces(ref board), type, color);
        }

        public static void RelocatePiece(Piece[,] selectedBoard, PointInt currentPoint, PointInt nextPoint)
        {
            selectedBoard[nextPoint.Y, nextPoint.X] = selectedBoard[currentPoint.Y, currentPoint.X];
            selectedBoard[nextPoint.Y, nextPoint.X].ChessboardPointInt = nextPoint;
            selectedBoard[currentPoint.Y, currentPoint.X] = null;
        }
        public static void RelocatePiece(BoardCell[,] selectedBoard, PointInt currentPoint, PointInt nextPoint)
        {
            selectedBoard[nextPoint.Y, nextPoint.X].Piece = selectedBoard[currentPoint.Y, currentPoint.X].Piece;
            selectedBoard[nextPoint.Y, nextPoint.X].Piece.ChessboardPointInt = nextPoint;
            selectedBoard[currentPoint.Y, currentPoint.X].Piece = null;
        }
    }
}
