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
using WPFTestChess.HelpElems;
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess.PlayPage
{
    /// <summary>
    /// Interaction logic for GameChoiseView.xaml
    /// </summary>
    public partial class GameChoiseView : Page
    {
        GameChoiseViewModel viewModel;
        public GameChoiseView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = new GameChoiseViewModel(NavigationService.Navigate);

            this.DataContext = viewModel;

        }
        public void PageNavigate(MyButton myButton)
        {
            //NavigationService nav = NavigationService.GetNavigationService(this);
            NavigationService.Navigate(new Uri(@"PlayPage\Games\PlayOnlineView.xaml", UriKind.Relative));
        }
    }
}
