using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WPFTestChess.HelpElems;

namespace WPFTestChess.GlobalMenu
{
    internal class MenuElem : MyNotifyPropertyChanged
    {
        private MenuImage image;
        public MenuImage Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        private static Brush color = (SolidColorBrush)new BrushConverter().ConvertFrom("#272522");
        private static Brush selectedColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#22201f");

        public Brush Color
        {
            get => color;
        }

        public Brush SelectedColor
        {
            get => selectedColor;
        }

        public Brush TitleColor
        {
            get => Brushes.LightGray;
        }
        public Brush SelectedTitleColor
        {
            get => Brushes.White;
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand openWindowCmd;
        public ICommand OpenWindowCmd => openWindowCmd;

        private Uri windowUri;

        public MenuElem(string imageSource, string title,string uri, WindowNavigate windowNavigate)
        {
            Image = new MenuImage(imageSource);
            Title = title;

            windowUri = new Uri(uri, UriKind.Relative);

            openWindowCmd = new RelayCommand((t) => windowNavigate(windowUri), null);
        }

        public delegate bool WindowNavigate(Uri source);
    }
}
