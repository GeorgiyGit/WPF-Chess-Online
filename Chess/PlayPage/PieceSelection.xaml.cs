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
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess.PlayPage
{
    /// <summary>
    /// Interaction logic for PieceSelection.xaml
    /// </summary>
    public partial class PieceSelection :UserControl
    {
        //PieceSelectionViewModel viewModel;
        public PieceSelection()
        {
            InitializeComponent();

            //viewModel = new PieceSelectionViewModel();

            //viewModel.Color = PieceColors.Black;
        }
    }
}
