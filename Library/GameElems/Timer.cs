using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Library.HelpElems;
using Timer = System.Timers.Timer;

namespace Library.GameElems
{
    public class MyTimer:MyNotifyPropertyChanged
    {
        private Timer timer;

        private DateTime dateTime;
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime maxTime;
        public DateTime MaxTime
        {
            get
            {
                return maxTime;
            }
            set
            {
                maxTime = value;
                OnPropertyChanged();
            }
        }

        private TimerMax timerMax;

        public void StartTimer(int seconds)
        {
            timer.Interval = 1000;
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            dateTime.AddSeconds(1);
            if(dateTime>= maxTime)
            {
                timerMax.Invoke();
            }

        }

        delegate void TimerMax();
    }
}
