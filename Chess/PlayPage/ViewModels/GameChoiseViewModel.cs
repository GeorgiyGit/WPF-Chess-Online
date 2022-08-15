using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Library.HelpElems;
using WPFTestChess.HelpElems;

namespace WPFTestChess.PlayPage.ViewModels
{
    internal class GameChoiseViewModel
    {
        private ObservableCollection<MyButton> playMenu = new ObservableCollection<MyButton>();
        public IEnumerable<MyButton> PlayMenu => playMenu;

        public GameChoiseViewModel(ClickD cd)
        {
            Initialize(cd);
        }
        private void Initialize(ClickD cd)
        {
            AddPlayMenuElem("Play Online",
                            "Play vs a person of similar skill",
                            @"PlayPageResources\PlayOnlineImg.png",
                            @"PlayPage\Games\PlayOnlineView.xaml",
                            cd);

            AddPlayMenuElem("Play Computer",
                            "Challenge a bot",
                            @"PlayPageResources\PlayComputerImg.png",
                            @"PlayPage\Games\PlayBotView.xaml",
                            cd);

            AddPlayMenuElem("Play a Friend",
                            "Invite a friend to a game of chess",
                            @"PlayPageResources\PlayFriendImg.png",
                            @"PlayPage\Games\PlayOnlineView.xaml",
                            cd);

            AddPlayMenuElem("Tournaments",
                            "Join an Arena where anyone can win",
                            @"PlayPageResources\TournamentImg.png",
                            @"PlayPage\Games\PlayOnlineView.xaml",
                            cd);

            AddPlayMenuElem("Chess Variants",
                            "Find fun new ways to play chess",
                            @"PlayPageResources\VariantsImg.png",
                            @"PlayPage\Games\PlayOnlineView.xaml",
                            cd);
        }
        private void AddPlayMenuElem(string fCon, string sCon, string imgPath, string page, ClickD cd)
        {
            playMenu.Add(new MyButton(imgPath)
            {
                FirstLayerText = fCon,
                SecondLayerText = sCon,
                FirstTextBrush = Brushes.White,
                SecondTextBrush = Brushes.Gray,
                WindowPath = new Uri(page, UriKind.Relative),
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#1F1E1B"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#171614"),
                FirstBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#171614"),
                FirstBorderThickness = new System.Windows.Thickness(0, 0, 0, 2),
                ClickDel = cd.Invoke
            });
        }

        public delegate bool ClickD(Uri obj);
    }
}
