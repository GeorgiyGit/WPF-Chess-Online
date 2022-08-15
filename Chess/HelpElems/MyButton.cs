using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTestChess.HelpElems
{
    public class MyButton:MyNotifyPropertyChanged
    {
        #region Text
        private string firstLayerText;
        private string secondLayerText;
        private string thirdLayerText;

        public string FirstLayerText
        {
            get
            {
                return firstLayerText;
            }
            set
            {
                firstLayerText = value;
                OnPropertyChanged();
            }
        }
        public string SecondLayerText
        {
            get
            {
                return secondLayerText;
            }
            set
            {
                secondLayerText = value;
                OnPropertyChanged();
            }
        }
        public string ThirdLayerText
        {
            get
            {
                return thirdLayerText;
            }
            set
            {
                thirdLayerText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TextBrushes;
        private Brush firstTextBrush;
        private Brush secondTextBrush;
        private Brush thirdTextBrush;

        public Brush FirstTextBrush
        {
            get
            {
                return firstTextBrush;
            }
            set
            {
                firstTextBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SecondTextBrush
        {
            get
            {
                return secondTextBrush;
            }
            set
            {
                secondTextBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush ThirdTextBrush
        {
            get
            {
                return thirdTextBrush;
            }
            set
            {
                thirdTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedTextBrushes;
        private Brush selectedFirstTextBrush;
        private Brush selectedSecondTextBrush;
        private Brush selectedThirdTextBrush;

        public Brush SelectedFirstTextBrush
        {
            get
            {
                return selectedFirstTextBrush;
            }
            set
            {
                selectedFirstTextBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SelectedSecondTextBrush
        {
            get
            {
                return selectedSecondTextBrush;
            }
            set
            {
                selectedSecondTextBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SelectedThirdTextBrush
        {
            get
            {
                return selectedThirdTextBrush;
            }
            set
            {
                selectedThirdTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BorderThickness;
        private Thickness firstBorderThickness;
        private Thickness secondBorderThickness;
        private Thickness thirdBorderThickness;

        public Thickness FirstBorderThickness
        {
            get
            {
                return firstBorderThickness;
            }
            set
            {
                firstBorderThickness = value;
                OnPropertyChanged();
            }
        }
        public Thickness SecondBorderThickness
        {
            get
            {
                return secondBorderThickness;
            }
            set
            {
                secondBorderThickness = value;
                OnPropertyChanged();
            }
        }
        public Thickness ThirdBorderThickness
        {
            get
            {
                return thirdBorderThickness;
            }
            set
            {
                thirdBorderThickness = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BorderBrushes;
        private Brush firstBorderBrush;
        private Brush secondBorderBrush;
        private Brush thirdBorderBrush;

        public Brush FirstBorderBrush
        {
            get
            {
                return firstBorderBrush;
            }
            set
            {
                firstBorderBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SecondBorderBrush
        {
            get
            {
                return secondBorderBrush;
            }
            set
            {
                secondBorderBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush ThirdBorderBrush
        {
            get
            {
                return thirdBorderBrush;
            }
            set
            {
                thirdBorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SeletedBorderBrushes;
        private Brush selectedFirstBorderBrush;
        private Brush selectedSecondBorderBrush;
        private Brush selectedThirdBorderBrush;

        public Brush SelectedFirstBorderBrush
        {
            get
            {
                return selectedFirstBorderBrush;
            }
            set
            {
                selectedFirstBorderBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SelectedSecondBorderBrush
        {
            get
            {
                return selectedSecondBorderBrush;
            }
            set
            {
                selectedSecondBorderBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SelectedThirdBorderBrush
        {
            get
            {
                return selectedThirdBorderBrush;
            }
            set
            {
                selectedThirdBorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BackgroundBrushes;
        private Brush backBrush;
        private Brush selectedBackBrush;
        public Brush BackBrush
        {
            get
            {
                return backBrush;
            }
            set
            {
                backBrush = value;
                OnPropertyChanged();
            }
        }
        public Brush SelectedBackBrush
        {
            get
            {
                return selectedBackBrush;
            }
            set
            {
                selectedBackBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion;

        #region Commands;
        private HelpElems.RelayCommand clickCmd;
        public ICommand ClickCmd => clickCmd;
        #endregion

        #region Images;
        private BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private Uri windowPath;
        public Uri WindowPath
        {
            get
            {
                return windowPath;
            }
            set
            {
                windowPath = value;
                OnPropertyChanged();
            }
        }

        private Page page;
        public Page Page
        {
            get
            {
                return page;
            }
            set
            {
                page = value;
                OnPropertyChanged();
            }
        }

        #region Start;
        public MyButton()
        {
            clickCmd = new HelpElems.RelayCommand((t) => ClickDel.Invoke(WindowPath), null);
        }
        public MyButton(string imgPath)
        {
            Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(imgPath, UriKind.Relative);
            Image.EndInit();

            clickCmd = new HelpElems.RelayCommand((t) => ClickDel.Invoke(WindowPath), null);
        }

        public MyButton(Click click, CanClick c)
        {
            clickCmd = new HelpElems.RelayCommand((t) => click.Invoke(), (t) => c.Invoke());
        }
        public MyButton(Click click)
        {
            clickCmd = new HelpElems.RelayCommand((t) => click.Invoke(), null);
        }
        #endregion

        private ClickD clickDel;
        public ClickD ClickDel
        {
            get
            {
                return clickDel;
            }
            set
            {
                clickDel = value;
                OnPropertyChanged();
            }
        }

        public delegate bool ClickD(Uri obj);

        public delegate void Click();
        public delegate bool CanClick();
    }
}
