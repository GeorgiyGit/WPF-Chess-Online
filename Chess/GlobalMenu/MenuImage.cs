using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFTestChess.GlobalMenu
{
    internal class MenuImage : MyNotifyPropertyChanged
    {
        private BitmapImage source;
        public BitmapImage Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        public MenuImage() { }
        public MenuImage(string source)
        {
            Source = new BitmapImage();

            Source.BeginInit();
            Source.UriSource = new Uri(source, UriKind.Relative);
            Source.EndInit();
        }
    }
}
