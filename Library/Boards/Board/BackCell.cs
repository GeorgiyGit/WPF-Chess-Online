using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Library.Boards.BoardNS
{
    public class BackCell : INotifyPropertyChanged
    {
        public static Brush BlackCellColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF769656");
        public static Brush WhiteCellColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEEEED2");

        public static Brush BlackCellColorSelected = (SolidColorBrush)new BrushConverter().ConvertFrom("#BACA2B");
        public static Brush WhiteCellColorSelected = (SolidColorBrush)new BrushConverter().ConvertFrom("#F6F669");

        private static Brush StrokeColorSelected = (SolidColorBrush)new BrushConverter().ConvertFrom("#E1E4C4");

        private Brush circleColor = Brushes.Gray;
        private Brush attakCircleColor = Brushes.Red;
        public Brush CircleColor
        {
            get
            {
                return isAttackCircle ? attakCircleColor : circleColor;
            }
            set
            {
                circleColor = value;
                OnPropertyChanged();
            }
        }

        private bool isAttackCircle;
        public bool IsAttackCircle
        {
            get
            {
                return isAttackCircle;
            }
            set
            {
                isAttackCircle = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CircleOpacity));
                OnPropertyChanged(nameof(CircleRadius));
                OnPropertyChanged(nameof(CircleColor));
            }
        }
        private const double OPACITY = 0.5d;
        private const double CIRCLE_RADIUS = 2d;

        private const double stroke_thickness = 2d;
        public double STROKE_THICKNESS
        {
            get { return stroke_thickness; }
        }

        private bool isWhite;
        public bool IsWhite
        {
            get
            {
                return isWhite;
            }
            set
            {
                isWhite = value;
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(BackColor));
                OnPropertyChanged(nameof(StrokeColor));
            }
        }



        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(BackColor));
                OnPropertyChanged(nameof(StrokeColor));
            }
        }

        private bool isPressed;
        public bool IsPressedTrue
        {
            get
            {
                return isPressed;
            }
            set
            {
                isPressed = true;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(BackColor));
                OnPropertyChanged(nameof(StrokeColor));

                //isFirstPress = false;
            }
        }
        public bool IsPressedFalse
        {
            get
            {
                return isPressed;
            }
            set
            {
                isPressed = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(BackColor));
                OnPropertyChanged(nameof(StrokeColor));

                //isFirstPress = false;
            }
        }

        private bool isCircle;
        public bool IsCircle
        {
            get
            {
                return isCircle;
            }
            set
            {
                isCircle = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CircleOpacity));
                OnPropertyChanged(nameof(CircleRadius));
                OnPropertyChanged(nameof(CircleColor));
            }
        }
        public Brush BackColor
        {
            get
            {
                return IsSelected || isPressed ? IsWhite ? WhiteCellColorSelected : BlackCellColorSelected : IsWhite ? WhiteCellColor : BlackCellColor;
            }
        } 
        public Brush StrokeColor
        {
            get
            {
                return IsSelected || isPressed ? StrokeColorSelected : BackColor;
            }
        }

        public double CircleOpacity
        {
            get
            {
                return IsCircle ? OPACITY : 0d;
            }
        }
        public double CircleRadius
        {
            get
            {
                return IsCircle ? CIRCLE_RADIUS*20 : 0d;
            }
        }

        public BackCell(bool isWhite, bool isSelected)
        {
            this.isWhite = isWhite;
            this.isSelected = isSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
