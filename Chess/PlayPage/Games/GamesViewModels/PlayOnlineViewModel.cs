using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestChess.PlayPage.ViewModels;
using Library.HelpElems;
using Library.GameElems;

namespace WPFTestChess.PlayPage.Games.GamesViewModels
{
    internal class PlayOnlineViewModel : MyNotifyPropertyChanged
    {
        private OnlineChessboardViewModel chessboard_ViewModel;
        public OnlineChessboardViewModel Chessboard_ViewModel
        {
            get
            {
                return chessboard_ViewModel;
            }
            set
            {
                chessboard_ViewModel = value;
                OnPropertyChanged();
            }
        }

        public PlayOnlineViewModel(NavigateDel nav)
        {
            this.nav = nav;
        }
        public void Start(GameColor start)
        {
            Chessboard_ViewModel = new OnlineChessboardViewModel(start, nav.Invoke);
        }

        NavigateDel nav;
        public delegate bool NavigateDel(Uri obj);
    }
}
