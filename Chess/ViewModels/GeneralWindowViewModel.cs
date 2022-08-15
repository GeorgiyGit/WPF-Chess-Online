using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestChess.GlobalMenu;

namespace WPFTestChess
{
    internal class GeneralWindowViewModel : MyNotifyPropertyChanged
    {

        private GlobalMenuViewModel global_Menu_ViewModel;
        public GlobalMenuViewModel Global_Menu_ViewModel
        {
            get
            {
                return global_Menu_ViewModel;
            }
            set
            {
                global_Menu_ViewModel = value;
                OnPropertyChanged();
            }
        }

        public GeneralWindowViewModel(WindowNavigate windowNavigate)
        {
            Global_Menu_ViewModel = new GlobalMenuViewModel(windowNavigate.Invoke);

            windowNavigate.Invoke(new Uri(@"HomeWindow\HomeWindow.xaml", UriKind.Relative));
        }

        public delegate bool WindowNavigate(Uri source);
    }
}
