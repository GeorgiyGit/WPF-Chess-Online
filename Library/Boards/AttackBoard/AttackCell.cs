using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Boards.AttackBoardNS
{
    public class AttackCell
    {
        public List<Piece> pieces = new List<Piece>();

        public PieceColors color;

        public bool IsAttack { get; set; }

        public AttackCell(PieceColors color)
        {
            this.color = color;
        }
        public void AddPieces(Piece piece, bool isAttack)
        {
            IsAttack = isAttack;
            if (piece != null) if (piece.Color == color) pieces.Add(piece);
        }

    }
}
