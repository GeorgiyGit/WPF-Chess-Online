using Library.Boards;
using Library.Boards.BoardNS;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpElems
{
    public static class Converter
    {
        public static ObservableCollection<BoardCell> TwoDemensionalArrayToCollection(BoardCell[,] arr)
        {
            ObservableCollection<BoardCell> result = new ObservableCollection<BoardCell>();

            if (Chessboard.IsWhiteButton)
            {
                for (int y = 0; y < arr.GetLength(0); y++)
                {
                    for (int x = 0; x < arr.GetLength(1); x++)
                    {
                        result.Add(arr[Chessboard.CELL_COUNT - y - 1, x]);
                    }
                }
            }
            else
            {
                foreach (var item in arr)
                {
                    result.Add(item);
                }
            }
            return result;
        }


        public static Piece[,] BoardCellsToPieces(ref BoardCell[,] board)
        {
            Piece[,] pBoard = new Piece[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    pBoard[y, x] = board[y, x]?.Piece;
                }
            }
            return pBoard;
        }
    }
}
