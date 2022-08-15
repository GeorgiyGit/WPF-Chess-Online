using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.HelpElems;
using WPFTestChess.PlayPage.Games;

namespace WPFTestChess.PlayPage.ViewModels
{
    internal class GameViewModel:MyNotifyPropertyChanged
    {
        #region Properties
        private ChessboardViewModel chessboard_View_Model;
        public ChessboardViewModel Chessboard_View_Model
        {
            get
            {
                return chessboard_View_Model;
            }
            set
            {
                chessboard_View_Model = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
