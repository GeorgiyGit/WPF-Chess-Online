using Library.Boards.AttackBoardNS;
using Library.GameElems;
using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Boards.BoardNS
{
    public class BotBoard : ServerChessboard
    {
        public BoardCell[,] board = new BoardCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];
        AttackBoard attackBoard;
        public BotBoard(MouseClickD execute, IsMouseClickD canExecute)
        {

            Initialization(board, execute, canExecute);
            //Initialization(checkBoard, execute, canExecute);

            attackBoard = new AttackBoard();
        }
        private void Initialization(BoardCell[,] initialBoard, MouseClickD execute, IsMouseClickD canExecute)
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    bool isWhite = y % 2 == 0 ? x % 2 == 0 ? false : true : x % 2 == 0 ? true : false;

                    initialBoard[y, x] = new BoardCell(new BackCell(isWhite, false), execute.Invoke, canExecute.Invoke, new PointInt(x, y));
                }
            }
            InitializePiece(ref initialBoard);

        }

        public void ReplacePiece(Step step)
        {
            board[step.NextPoint.Y, step.NextPoint.X].Piece = board[step.CurrentPoint.Y, step.CurrentPoint.X].Piece;
            board[step.NextPoint.Y, step.NextPoint.X].Piece.ChessboardPointInt = step.NextPoint;
            board[step.CurrentPoint.Y, step.CurrentPoint.X].Piece = null;
        }



        public void ClearPress()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    board[y, x].BackCell.IsPressedFalse = false;
                }
            }
        }

        public void ClearCircle()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    board[y, x].BackCell.IsCircle = false;
                }
            }
        }

        public void RoqueMove(Step step)
        {
            int moveVector = step.CurrentPoint.X > step.NextPoint.X ? -2 : 2;
            int moveVector2 = step.CurrentPoint.X > step.NextPoint.X ? -1 : 1;

            RelocatePiece(board, step.CurrentPoint, new PointInt(step.CurrentPoint.X + moveVector, step.CurrentPoint.Y));
            RelocatePiece(board, step.NextPoint, new PointInt(step.CurrentPoint.X + moveVector2, step.CurrentPoint.Y));

            ((King)board[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector].Piece).isFirstMove = false;
            ((Rook)board[step.NextPoint.Y, step.CurrentPoint.X + moveVector2].Piece).isFirstMove = false;
        }






        public void StartNewGame()
        {
            for (int x = 0; x < Chessboard.CELL_COUNT; x++)
            {
                for (int y = 0; y < Chessboard.CELL_COUNT; y++)
                {
                    board[x, y].Piece = null;
                    //checkBoard[x, y].Piece = null;
                }
            }
            InitializePiece(ref board);
            //InitializePiece(Converter.BoardCellsToPieces(checkBoard));
            gameState = GameState.NoWiners;
        }

        public void InitializePiece(ref BoardCell[,] initialBoard)
        {
            for (int i = 0; i < Chessboard.CELL_COUNT; i++)
            {
                initialBoard[1, i].Piece = new Pawn(PieceColors.White, PieceType.Pawn, @"Resources\Pieces\White\Pawn.png", new PointInt(i, 1));
                initialBoard[6, i].Piece = new Pawn(PieceColors.Black, PieceType.Pawn, @"Resources\Pieces\Black\Pawn.png", new PointInt(i, 6));

                (initialBoard[1, i].Piece as Pawn).isFirstMove = true;
                (initialBoard[6, i].Piece as Pawn).isFirstMove = true;
            }


            initialBoard[0, 0].Piece = new Rook(PieceColors.White, PieceType.Rook, @"Resources\Pieces\White\Rook.png", new PointInt(0, 0));
            initialBoard[0, 7].Piece = new Rook(PieceColors.White, PieceType.Rook, @"Resources\Pieces\White\Rook.png", new PointInt(7, 0));

            initialBoard[7, 0].Piece = new Rook(PieceColors.Black, PieceType.Rook, @"Resources\Pieces\Black\Rook.png", new PointInt(0, 7));
            initialBoard[7, 7].Piece = new Rook(PieceColors.Black, PieceType.Rook, @"Resources\Pieces\Black\Rook.png", new PointInt(7, 7));


            initialBoard[0, 1].Piece = new Knight(PieceColors.White, PieceType.Knight, @"Resources\Pieces\White\Knight.png", new PointInt(1, 0));
            initialBoard[0, 6].Piece = new Knight(PieceColors.White, PieceType.Knight, @"Resources\Pieces\White\Knight.png", new PointInt(6, 0));

            initialBoard[7, 1].Piece = new Knight(PieceColors.Black, PieceType.Knight, @"Resources\Pieces\Black\Knight.png", new PointInt(1, 7));
            initialBoard[7, 6].Piece = new Knight(PieceColors.Black, PieceType.Knight, @"Resources\Pieces\Black\Knight.png", new PointInt(6, 7));


            initialBoard[0, 2].Piece = new Bishop(PieceColors.White, PieceType.Bishop, @"Resources\Pieces\White\Bishop.png", new PointInt(2, 0));
            initialBoard[0, 5].Piece = new Bishop(PieceColors.White, PieceType.Bishop, @"Resources\Pieces\White\Bishop.png", new PointInt(5, 0));

            initialBoard[7, 2].Piece = new Bishop(PieceColors.Black, PieceType.Bishop, @"Resources\Pieces\Black\Bishop.png", new PointInt(2, 7));
            initialBoard[7, 5].Piece = new Bishop(PieceColors.Black, PieceType.Bishop, @"Resources\Pieces\Black\Bishop.png", new PointInt(5, 7));


            initialBoard[0, 3].Piece = new Queen(PieceColors.White, PieceType.Queen, @"Resources\Pieces\White\Queen.png", new PointInt(3, 0));
            initialBoard[7, 3].Piece = new Queen(PieceColors.Black, PieceType.Queen, @"Resources\Pieces\Black\Queen.png", new PointInt(3, 7));


            initialBoard[0, 4].Piece = new King(PieceColors.White, PieceType.King, @"Resources\Pieces\White\King.png", new PointInt(4, 0));
            initialBoard[7, 4].Piece = new King(PieceColors.Black, PieceType.King, @"Resources\Pieces\Black\King.png", new PointInt(4, 7));
        }


        public delegate void MouseClickD(object obj);
        public delegate bool IsMouseClickD(object obj);
    }
}