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
using WPFTestChess.ViewModels;

namespace WPFTestChess
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        LogInViewModel v;
        public LogIn()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            v = new LogInViewModel(NavigationService.Navigate);
            this.DataContext = v;
        }
        PasswordBox passBox;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passBox = (PasswordBox)sender;
            v.PasswordTextBox.Text = ((PasswordBox)sender).Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passBox != null) passBox.Password = v.PasswordTextBox.Text;
        }
    }
}
