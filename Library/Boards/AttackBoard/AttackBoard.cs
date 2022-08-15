using Library.Boards.BoardNS;
using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Boards.AttackBoardNS
{
    public class AttackBoard
    {
        public AttackCell[,] Whiteboard = new AttackCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];

        public AttackCell[,] Blackboard = new AttackCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];


        public AttackBoard()
        {
        }

        public List<Piece> GetPieces(PointInt point, PieceColors color)
        {
            if (color == PieceColors.White)
                if (Whiteboard[point.Y, point.X] != null) return Whiteboard[point.Y, point.X].pieces;
                else if (Blackboard[point.Y, point.X] != null) return Blackboard[point.Y, point.X].pieces;
            return null;
        }

        public void PointsCalculation(Piece[,] board) //Calculate move and attack points
        {
            Whiteboard = new AttackCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];
            Blackboard = new AttackCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    Whiteboard[y, x] = new AttackCell(PieceColors.White);
                    Blackboard[y, x] = new AttackCell(PieceColors.Black);
                }
            }
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    if (board[y, x] != null)
                    {
                        board[y, x].Move(board);
                        if (board[y, x].Color == PieceColors.White) AddPiece(ref Whiteboard, board[y, x]);
                        else AddPiece(ref Blackboard, board[y, x]);
                    }
                }
            }
        }
        public void PointsCalculation(BoardCell[,] board) => PointsCalculation(Converter.BoardCellsToPieces(ref board));

        private void AddPiece(ref AttackCell[,] board, Piece piece)
        {
            for(int i=0;i<piece.attackPointInts.Count;i++)
                board[piece.attackPointInts[i].Y, piece.attackPointInts[i].X]?.AddPieces(piece, true);

            for (int i = 0; i < piece.movePointInts.Count; i++)
                board[piece.movePointInts[i].Y, piece.movePointInts[i].X]?.AddPieces(piece, false);
        }

        public KingState GetKingState(Piece[,] board, PieceColors color)
        {
            if (color == PieceColors.White) return GetKingStateByColor(Blackboard, board, color);
            return GetKingStateByColor(Whiteboard, board, color);
        }
        private KingState GetKingStateByColor(AttackCell[,] attackBoard, Piece[,] board, PieceColors color)
        {
            PointInt kingPoint = GetKingPoint(board, color);

            if (kingPoint.X >= 0 && kingPoint.X < Chessboard.CELL_COUNT && kingPoint.Y >= 0 && kingPoint.Y < Chessboard.CELL_COUNT)
            {
                if (attackBoard[kingPoint.Y, kingPoint.X]?.pieces?.Count > 0) return KingState.Check;
                return KingState.Normal;
            }
            return KingState.Error;
        }

        public KingState GetKingState(BoardCell[,] board, PieceColors color) => GetKingState(Converter.BoardCellsToPieces(ref board), color);

        private PointInt GetKingPoint(Piece[,] board, PieceColors color)
        {
            return Board.GetPiecesPoint(board, PieceType.King, color)[0];
        }
    }
}
