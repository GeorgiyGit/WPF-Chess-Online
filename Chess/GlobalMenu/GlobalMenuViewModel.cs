using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTestChess.HelpElems;

namespace WPFTestChess.GlobalMenu
{
    internal class GlobalMenuViewModel : MyNotifyPropertyChanged
    {
        private ObservableCollection<MyMenuItem> menuItems=new ObservableCollection<MyMenuItem>();
        public IEnumerable<MyMenuItem> MenuItems => menuItems;

        public ObservableCollection<MenuElem> exMenuItems;
        public IEnumerable<MenuElem> ExMenuItems => exMenuItems;

        public GlobalMenuViewModel(WindowNavigate windowNavigate)
        {
            Initialization(windowNavigate);
        }

        #region Properties
        private MenuElem topMenuImage;
        public MenuElem TopMenuImage
        {
            get => topMenuImage;
            set
            {
                topMenuImage = value;
                OnPropertyChanged();
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }


        private MyButton signUpButton;
        public MyButton SignUpButton
        {
            get
            {
                return signUpButton;
            }
            set
            {
                signUpButton = value;
                OnPropertyChanged();
            }
        }

        private MyButton logInButton;
        public MyButton LogInButton
        {
            get
            {
                return logInButton;
            }
            set
            {
                logInButton = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private RelayCommand menuItemClickCmd;
        public ICommand MenuItemClickCmd => menuItemClickCmd;

        private RelayCommand menuItemEnterCmd;
        public ICommand MenuItemEnterCmd => menuItemClickCmd;
        #endregion


        public void Initialization(WindowNavigate windowNavigate)
        {
            PlayInitial(windowNavigate);
            LearnInitial(windowNavigate);
            WatchInitial(windowNavigate);
            NewsInitial(windowNavigate);
            SocialInitial(windowNavigate);
            MoreInitial(windowNavigate);

            ButtonsInitial(windowNavigate);

            TopMenuImage = new MenuElem(@"\Resources\Menu\TopMenuImg.png", "", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);
        }

        private void ButtonsInitial(WindowNavigate windowNavigate)
        {
            SignUpButton = new MyButton()
            {
                FirstLayerText = "Sign Up",
                FirstTextBrush = Brushes.LightGray,
                SelectedFirstTextBrush = Brushes.White,
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#383634"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#4A4846"),
                ClickDel=windowNavigate.Invoke,
                WindowPath=new Uri(@"SignUp.xaml", UriKind.Relative)
            };

            LogInButton = new MyButton()
            {
                FirstLayerText = "Log In",
                FirstTextBrush = Brushes.White,
                SelectedFirstTextBrush = Brushes.White,
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#7FA650"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#95BB4A"),
                ClickDel = windowNavigate.Invoke,
                WindowPath = new Uri(@"LogIn.xaml", UriKind.Relative)
            };
        }

        private void PlayInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\PlayMenu\PlayImg.png", "Play", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);
            MenuElemsAdd(menuElems, @"\Resources\Menu\PlayMenu\ComputerImg.png", "Computer", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);
            MenuElemsAdd(menuElems, @"\Resources\Menu\PlayMenu\LeaderboardImg.png", "Leaderboard", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\PlayMenu\PlayImg.png", "Play", @"PlayPage\GameChoiseView.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }
        private void LearnInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\LearnImg.png", "Lessons", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\LearnImg.png", "Learn", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }
        private void WatchInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\WatchImg.png", "Streamers", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\WatchImg.png", "Watch", @"WatchView.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }
        private void NewsInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\NewsImg.png", "News", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\NewsImg.png", "News", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }
        private void SocialInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\SocialMenu\ForumImg.png", "Forum", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\SocialMenu\SocialImg.png", "Social", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }
        private void MoreInitial(WindowNavigate windowNavigate)
        {
            ObservableCollection<MenuElem> menuElems = new ObservableCollection<MenuElem>();

            MenuElemsAdd(menuElems, @"\Resources\Menu\MoreMenu\LibraryImg.png", "Library", @"HomeWindow\HomeWindow.xaml", windowNavigate.Invoke);

            menuItems.Add(new MyMenuItem(new MenuElem(@"\Resources\Menu\MoreMenu\MoreImg.png", "More", @"MainWindow.xaml", windowNavigate.Invoke),
                                         menuElems,
                                         MenuItemEnter));
        }

        private void MenuElemsAdd(ObservableCollection<MenuElem> menuElems,string source, string title,string uri, WindowNavigate windowNavigate)
        {
            menuElems.Add(new MenuElem(source, title, uri, windowNavigate.Invoke));
        }

        public void MenuItemEnter(object obj)
        {
            var item = obj as ObservableCollection<MenuElem>;
            exMenuItems = item;
            IsExpanded = true;
        }


        public delegate bool WindowNavigate(Uri source);
    }
}
