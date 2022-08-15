using Library.Boards.BoardNS;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFTestChess.PlayPage.ViewModels
{
    internal class PieceSelectionViewModel : INotifyPropertyChanged
    {
        private Brush strokeColorSelected = (SolidColorBrush)new BrushConverter().ConvertFrom("#E1E4C4");

        private PieceColors color;
        public PieceColors Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BorderBrush));

                OnPropertyChanged(nameof(BishopSource));
                OnPropertyChanged(nameof(KnightSource));
                OnPropertyChanged(nameof(RookSource));
                OnPropertyChanged(nameof(QueenSource));
                OnPropertyChanged(nameof(BackBrush));
            }
        }
        public Brush BorderBrush
        {
            get
            {
                return strokeColorSelected;
            }
        }

        public Brush BackBrush
        {
            get
            {
                return Color == PieceColors.White ? BackCell.WhiteCellColor : BackCell.BlackCellColor;
            }
        }
        private IconSource bishopSource;
        private IconSource knightSource;
        private IconSource rookSource;
        private IconSource queenSource;

        public ImageSource BishopSource{ get => bishopSource.GetSource(Color); }
        public ImageSource KnightSource { get => knightSource.GetSource(Color); }
        public ImageSource RookSource { get => rookSource.GetSource(Color); }
        public ImageSource QueenSource { get => queenSource.GetSource(Color); }

        private double length;
        public double Length
        {
            get => length;
            set
            {
                if (value != double.NaN)
                {
                    length = value;
                    OnPropertyChanged();
                }
            }
        }

        private Point canvasPoint;
        public Point CanvasPoint
        {
            get
            {
                return canvasPoint;
            }
            set
            {
                canvasPoint = value;
                OnPropertyChanged();
            }
        }

        private SelectedPiece selectedPiece;

        HelpElems.RelayCommand bishopCmd;
        HelpElems.RelayCommand knightCmd;
        HelpElems.RelayCommand rookCmd;
        HelpElems.RelayCommand queenCmd;

        public ICommand BishopCmd => bishopCmd;
        public ICommand KnightCmd => knightCmd;
        public ICommand RookCmd => rookCmd;
        public ICommand QueenCmd => queenCmd;

        public PieceSelectionViewModel(PieceColors color, Point point, SelectedPiece selectedPiece)
        {
            bishopSource = new IconSource(@"Resources\Pieces\White\Bishop.png", @"Resources\Pieces\Black\Bishop.png", PieceType.Bishop);
            knightSource = new IconSource(@"Resources\Pieces\White\Knight.png", @"Resources\Pieces\Black\Knight.png", PieceType.Knight);
            rookSource = new IconSource(@"Resources\Pieces\White\Rook.png", @"Resources\Pieces\Black\Rook.png", PieceType.Rook);
            queenSource = new IconSource(@"Resources\Pieces\White\Queen.png", @"Resources\Pieces\Black\Queen.png", PieceType.Queen);

            Length = 200;

            CanvasPoint = point;
            Color = color;
            this.selectedPiece = selectedPiece;

            bishopCmd = new HelpElems.RelayCommand((t) => BishopEx(), null);
            knightCmd = new HelpElems.RelayCommand((t) => KnightEx(), null);
            rookCmd = new HelpElems.RelayCommand((t) => RookEx(), null);
            queenCmd = new HelpElems.RelayCommand((t) => QueenEx(), null);

        }


        public void BishopEx() => selectedPiece.Invoke(PieceType.Bishop);
        public void KnightEx() => selectedPiece.Invoke(PieceType.Knight);
        public void RookEx() => selectedPiece.Invoke(PieceType.Rook);
        public void QueenEx() => selectedPiece.Invoke(PieceType.Queen);


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public delegate void SelectedPiece(PieceType type);
    }
}
