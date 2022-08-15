using Library.GameElems;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;
using WPFTestChess.HelpElems;
using WPFTestChess.PlayPage.Games.GamesViewModels;
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess.PlayPage
{
    /// <summary>
    /// Interaction logic for PlayOnlineView.xaml
    /// </summary>
    public partial class PlayOnlineView : Page
    {
        PlayOnlineViewModel v;
        public PlayOnlineView()
        {
            InitializeComponent();

            
        }

        private void GameStartConnect()
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    byte[] data = new byte[128];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = MyClient.Stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (MyClient.Stream.DataAvailable);

                    Message message = Message.Deserialize(builder.ToString());
                    if (message.Type == typeof(GameColor))
                    {
                        GameColor color = message.Value.ToObject<GameColor>();

                        //ChessboardViewModel.OurColor = color.Color;

                        v.Start(color);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MyClient.User != null)
            {
                v = new PlayOnlineViewModel(NavigationService.Navigate);
                this.DataContext = v;
                try
                {
                    GameStart gameStart = new GameStart();
                    string data = Message.Serialize(Message.FromValue(gameStart));

                    byte[] data2 = Encoding.Unicode.GetBytes(data);
                    MyClient.Stream.Write(data2, 0, data2.Length);

                    Thread receiveThread = new Thread(new ThreadStart(GameStartConnect));
                    receiveThread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height - 100 > 0 && MainGrid.ColumnDefinitions[1].ActualWidth > 0)
            {
                Chessboard.Height = e.NewSize.Height  < MainGrid.ColumnDefinitions[1].ActualWidth ? e.NewSize.Height  : MainGrid.ColumnDefinitions[1].ActualWidth;
                Chessboard.Width = e.NewSize.Height  < MainGrid.ColumnDefinitions[1].ActualWidth ? e.NewSize.Height  : MainGrid.ColumnDefinitions[1].ActualWidth;
            }
        }
    }
}
