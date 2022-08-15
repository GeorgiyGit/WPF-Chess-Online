using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestChess.HelpElems;
using Library.HelpElems;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Library.GameElems;
using System.Threading;
using Db;
using System.Windows.Threading;
using System.Windows;

namespace WPFTestChess.ViewModels
{
    internal class SignUpViewModel:MyNotifyPropertyChanged
    {
        private MyTextBox usernameTextBox;
        public MyTextBox UsernameTextBox
        {
            get
            {
                return usernameTextBox;
            }
            set
            {
                usernameTextBox = value;
                OnPropertyChanged();
            }
        }

        private MyTextBox emailTextBox;
        public MyTextBox EmailTextBox
        {
            get
            {
                return emailTextBox;
            }
            set
            {
                emailTextBox = value;
                OnPropertyChanged();
            }
        }

        private MyTextBox passwordTextBox;
        public MyTextBox PasswordTextBox
        {
            get
            {
                return passwordTextBox;
            }
            set
            {
                passwordTextBox = value;
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

        public SignUpViewModel(NavigateDel nav)
        {
            navigate = nav;


            UsernameTextBox = new MyTextBox(@"\Resources\UsernameImg.png",
                                            @"\Resources\Correct.png",
                                            "Username",
                                            "Username must be between 3-25 characters long and use only Latin letters and numbers",
                                            IsUserNameCorrect);

            EmailTextBox = new MyTextBox(@"\Resources\EmailImg.png",
                                         @"\Resources\Correct.png",
                                         "Email",
                                         "This is not a valid email format",
                                         IsEmailCorrect);

            PasswordTextBox = new MyTextBox(@"\Resources\PasswordImg.png",
                                            @"\Resources\ShowPasswordImg.png",
                                            "Password",
                                            "Password should have at least 8 characters",
                                            IsPasswordCorrect);

            SignUpButton = new MyButton(Click, CanSignUp)
            {
                FirstLayerText = "Sign Up",
                FirstBorderThickness = new System.Windows.Thickness(5, 0, 5, 5),
                SecondBorderThickness = new System.Windows.Thickness(0, 0, 0, 5),
                FirstBorderBrush = Brushes.Black,
                SelectedFirstBorderBrush = Brushes.LightGreen,
                SecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#537133"),
                SelectedSecondBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#537133"),
                BackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#7FA650"),
                SelectedBackBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#95BB4A")
            };
        }

        public bool IsUserNameCorrect(MyTextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text, @"^\S{3,25}$"))
            {
                textBox.Incorrect = false;
                textBox.Correct = true;
                return true;
            }
            else if (textBox.Correct)
            {
                textBox.Incorrect = true;
                textBox.Correct = false;
                return false;
            }
            return false;
        }

        public bool IsEmailCorrect(MyTextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
            {
                textBox.Incorrect = false;
                textBox.Correct = true;
                return true;
            }
            else if (textBox.Correct)
            {
                textBox.Incorrect = true;
                textBox.Correct = false;
                return false;
            }
            return false;
        }

        public bool IsPasswordCorrect(MyTextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text, @"^\S{8,}$"))
            {
                textBox.Incorrect = false;
                textBox.Correct = true;
                return true;
            }
            else if (textBox.Correct)
            {
                textBox.Incorrect = true;
                textBox.Correct = false;
                return false;
            }
            return false;
            
        }

        public void Click()
        {
            SignUpMsg step = new SignUpMsg(UsernameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text);
            string data = Message.Serialize(Message.FromValue(step));
            try
            {
                byte[] data2 = Encoding.Unicode.GetBytes(data);
                MyClient.Stream.Write(data2, 0, data2.Length);


                Thread receiveThread = new Thread(new ThreadStart(GetUser));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetUser()
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

            if (message.Type == typeof(User))
            {
                User user = message.Value.ToObject<User>();

                MyClient.User = user;

                //GetUser();

                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                    new Action(() =>
                    {
                        navigate.Invoke(new Uri(@"HomeWindow\HomeWindow.xaml", UriKind.Relative));
                    }));
            }
            else if (message.Type == typeof(Rating))
            {
                Rating r = message.Value.ToObject<Rating>();

                MyClient.User.Rating = r;
            }
            else if (message.Type == typeof(SignLogError))
            {
                SignLogError error = message.Value.ToObject<SignLogError>();
            }
        }

        public bool CanSignUp()
        {
            if (UsernameTextBox.Correct && EmailTextBox.Correct && PasswordTextBox.Correct) return true;
            return false;
        }

        NavigateDel navigate;
        public delegate bool NavigateDel(Uri uri);
    }
}
