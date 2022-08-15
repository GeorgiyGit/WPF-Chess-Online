using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WPFTestChess.PlayPage.Games;
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        ChessboardViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            //viewModel = new ChessboardViewModel(new Library.GameElems.GameColor { Color = Library.HelpElems.PieceColors.White });

            this.DataContext = viewModel;

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height-100 > 0 && MainGrid.ColumnDefinitions[1].ActualWidth > 0)
            {
                ChessboardGrid.Height = e.NewSize.Height - 100 < MainGrid.ColumnDefinitions[1].ActualWidth ? e.NewSize.Height - 100 : MainGrid.ColumnDefinitions[1].ActualWidth;
                ChessboardGrid.Width = e.NewSize.Height - 100 < MainGrid.ColumnDefinitions[1].ActualWidth ? e.NewSize.Height - 100 : MainGrid.ColumnDefinitions[1].ActualWidth;
            }
        }
    }
}
