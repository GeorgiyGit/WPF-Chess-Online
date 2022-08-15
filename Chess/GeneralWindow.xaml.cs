using Library.GameElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTestChess.HelpElems;
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess
{
    /// <summary>
    /// Interaction logic for GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        GeneralWindowViewModel viewModel;
        static Frame frame2;
        public GeneralWindow()
        {
            InitializeComponent();

            MyClient.Connect();

            frame2 = frame;
            viewModel = new GeneralWindowViewModel(frame.NavigationService.Navigate);

            this.DataContext = viewModel;

            Thread receiveThread = new Thread(new ThreadStart(MyClient.Connect));
        }
        public static void Navigate(string path)
        {
            frame2.NavigationService.Navigate(path);
        }
    }
}
