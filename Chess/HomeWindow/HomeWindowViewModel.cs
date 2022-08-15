using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WPFTestChess.GlobalMenu;
using WPFTestChess.HelpElems;

namespace WPFTestChess.HomeWindow
{
    internal class HomeWindowViewModel:MyNotifyPropertyChanged
    {
        private BitmapImage standartBoardImg;
        public BitmapImage StandartBoardImg
        {
            get { return standartBoardImg; }
            set
            {
                standartBoardImg = value;
                OnPropertyChanged();
            }
        }

        private MyButton playButton;
        public MyButton PlayButton
        {
            get { return playButton; }
            set
            {
                playButton = value;
                OnPropertyChanged();
            }
        }

        private MyButton computerButton;
        public MyButton ComputerButton
        {
            get { return computerButton; }
            set
            {
                computerButton = value;
                OnPropertyChanged();
            }
        }

        public HomeWindowViewModel(WindowNavigate wN)
        {
            standartBoardImg = new BitmapImage();
            standartBoardImg.BeginInit();
            standartBoardImg.UriSource = new Uri(@"\Resources\Standartboard.png", UriKind.Relative);
            standartBoardImg.EndInit();

            PlayButton = new MyButton(@"\Resources\Menu\PlayImg.png")
            {
                FirstLayerText = "Play Online",
                SecondLayerText = "Play with someone at your level",
                FirstTextBrush = Brushes.White,
                SecondTextBrush = Brushes.White,
                SelectedFirstTextBrush = Brushes.White,
                SelectedSecondTextBrush = Brushes.White,
                FirstBorderThickness = new System.Windows.Thickness(5, 0, 5, 5),
                SecondBorderThickness = new System.Windows.Thickness(0, 0, 0, 5),
                FirstBorderBrush = Brushes.Black,
                SelectedFirstBorderBrush = Brushes.LightGreen,
                SecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#537133"),
                SelectedSecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#537133"),
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#7FA650"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#95BB4A"),
                WindowPath = new Uri(@"PlayPage\GameChoiseView.xaml",UriKind.Relative),
                ClickDel=wN.Invoke
            };

            ComputerButton = new MyButton(@"\Resources\Menu\PlayMenu\ComputerImg.png")
            {
                FirstLayerText = "Play Computer",
                SecondLayerText = "Play vs bot",
                FirstTextBrush = Brushes.LightGray,
                SecondTextBrush = Brushes.LightGray,
                SelectedFirstTextBrush = Brushes.White,
                SelectedSecondTextBrush = Brushes.White,
                FirstBorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                SecondBorderThickness = new System.Windows.Thickness(0, 0, 0, 5),
                SecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#3C3936"),
                SelectedSecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#3C3936"),
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#413E3C"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#52504D"),
                WindowPath=new Uri(@"PlayPage\GameChoiseView.xaml",UriKind.Relative),
                ClickDel = wN.Invoke
            };
        }

        public delegate bool WindowNavigate(Uri source);
    }
}
