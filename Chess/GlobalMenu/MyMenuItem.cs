using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPFTestChess.GlobalMenu
{
    internal class MyMenuItem: MyNotifyPropertyChanged
    {
        private MenuElem firstLayerMenyItem;
        public MenuElem FirstLayerMenyItem
        {
            get
            {
                return firstLayerMenyItem;
            }
            set
            {
                firstLayerMenyItem = value;
                OnPropertyChanged();
            }
        }

        private bool isMouseEnter;
        public bool IsMouseEnter
        {
            get
            {
                return isMouseEnter;
            }
            set
            {
                isMouseEnter = value;
                if (value == true) menuItemEnter.Invoke(secondLayerMenu);
                else;//menuItemEnter.Invoke(null);
            }
        }

        private ObservableCollection<MenuElem> secondLayerMenu = new ObservableCollection<MenuElem>();
        public IEnumerable<MenuElem> SecondLayerMenu => secondLayerMenu;


        MenuItemEnter menuItemEnter;

        public MyMenuItem() : this(null, null,null) { }
        public MyMenuItem(MenuElem firstLayer, ObservableCollection<MenuElem> secondLayer, MenuItemEnter menuItemEnter)
        {
            FirstLayerMenyItem = firstLayer;
            secondLayerMenu = secondLayer;

            this.menuItemEnter = menuItemEnter;
        }


        public delegate void MenuItemEnter(object obj);
    }
}
