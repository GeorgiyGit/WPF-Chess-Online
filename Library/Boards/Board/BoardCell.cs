using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTestChess.HelpElems;

namespace Library.Boards.BoardNS
{
    public class BoardCell: INotifyPropertyChanged
    {
        public BackCell BackCell { get; set; }

        private Piece piece;
        public Piece Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                OnPropertyChanged();
            }
        }

        public PointInt Point { get; set; }

        public BoardCell(BackCell backCell, Piece piece, MouseClickD execute, IsMouseClickD canExecute,PointInt point)
        {
            BackCell = backCell;
            Piece = piece;

            clickCmd = new RelayCommand((t) => execute(this), null);

            this.Point = point;
        }
        public BoardCell(BackCell backCell, MouseClickD execute, IsMouseClickD canExecute, PointInt point)
        {
            BackCell = backCell;

            clickCmd = new RelayCommand((t) => execute(this), null);

            this.Point = point;
        }
        private RelayCommand clickCmd;
        public ICommand ClickCmd => clickCmd;

        public delegate void MouseClickD(object obj);
        public delegate bool IsMouseClickD(object obj);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
