using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpElems
{
    public enum GameState
    {
        WhiteWin,
        BlackWin,
        WhiteCheck,
        BlackCheck,
        NoWiners
    }

    public enum PieceColors
    {
        White = 1,
        Black
    }
    public enum PieceType
    {
        Pawn = 1,//пішка
        Knight,//Кінь
        Bishop,//Слон
        Rook,//Тура
        Queen,//Ферзь
        King,
        None
    }

    public enum KingState
    {
        Normal,
        Check,//Шаг
        CheckMate,//Мат
        Error
    }
}
