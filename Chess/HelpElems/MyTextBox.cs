using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Library.HelpElems;

namespace WPFTestChess.HelpElems
{
    internal class MyTextBox:MyNotifyPropertyChanged
    {
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
                if (value != "") IsAnyText = true;
                else IsAnyText = false;
                isIncorrect.Invoke(this);
            }
        }

        private bool incorrect;
        public bool Incorrect
        {
            get
            {
                return incorrect;
            }
            set
            {
                incorrect = value;
                OnPropertyChanged();
            }
        }

        private bool correct;
        public bool Correct
        {
            get
            {
                return correct;
            }
            set
            {
                correct = value;
                OnPropertyChanged();
            }
        }

        private bool isAnyText;
        public bool IsAnyText
        {
            get
            {
                return isAnyText;
            }
            set
            {
                isAnyText = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage firstImg;
        public BitmapImage FirstImg
        {
            get
            {
                return firstImg;
            }
            set
            {
                firstImg = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage secondImg;
        public BitmapImage SecondImg
        {
            get
            {
                return secondImg;
            }
            set
            {
                secondImg = value;
                OnPropertyChanged();
            }
        }

        private IsIncorrect isIncorrect;

        private string watermark;
        public string Watermark
        {
            get
            {
                return watermark;
            }
            set
            {
                watermark = value;
                OnPropertyChanged();
            }
        }

        private string errorText;
        public string ErrorText
        {
            get
            {
                return errorText;
            }
            set
            {
                errorText = value;
                OnPropertyChanged();
            }
        }

        private HelpElems.RelayCommand clickCmd;
        public ICommand ClickCmd => clickCmd;

        public MyTextBox(string firstImg, string secondImg, string watermark, string errorText,IsIncorrect isIncorrect)
        {
            FirstImg = new BitmapImage();

            FirstImg.BeginInit();
            FirstImg.UriSource = new Uri(firstImg, UriKind.Relative);
            FirstImg.EndInit();


            SecondImg = new BitmapImage();

            SecondImg.BeginInit();
            SecondImg.UriSource = new Uri(secondImg, UriKind.Relative);
            SecondImg.EndInit();


            Watermark = watermark;
            ErrorText = errorText;
            this.isIncorrect = isIncorrect;

            clickCmd = new HelpElems.RelayCommand((t) => Click(), null);
        }

        private bool isShow;
        public bool IsShow
        {
            get
            {
                return isShow;
            }
            set
            {
                isShow = value;
                OnPropertyChanged();
            }
        }

        public void Click() => IsShow = !IsShow;

        public delegate bool IsIncorrect(MyTextBox textBox);
    }
}
