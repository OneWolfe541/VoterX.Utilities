using VoterX.Utilities.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace VoterX.Utilities.Timers
{
    public class IdleTimer
    {
        //private DispatcherTimer dispatcherTimer;
        //private ClientIdleHandler _clientIdleHandler;

        private DispatcherTimer _timer;

        //private Frame _parent;
        //private Page _destination;

        public IdleTimer()
        {

        }

        public void StartLogOutTimer(double Duration, Frame Parent, Page Destination)
        {
            _timer = new DispatcherTimer
                (
                TimeSpan.FromSeconds(10),
                //DispatcherPriority.ApplicationIdle,// Or DispatcherPriority.SystemIdle
                DispatcherPriority.SystemIdle,
                (s, e) =>
                {
                    Parent.Navigate(Destination);
                    _timer.Stop();
                },
                Application.Current.Dispatcher
                );
        }

        public void StartLogOutTimer(double Duration, Page Destination)
        {
            var frame = NavigationMethods.FindParent<Frame>(Destination);
            _timer = new DispatcherTimer
                (
                TimeSpan.FromSeconds(10),
                DispatcherPriority.ApplicationIdle,// Or DispatcherPriority.SystemIdle
                (s, e) =>
                {
                    frame.Navigate(Destination);
                    _timer.Stop();
                },
                Application.Current.Dispatcher
                );
        }

        public void RestartTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        ////call for idle time
        //public void callForIdletime(double Duration, Frame Parent, Page Destination)
        //{

        //    _parent = Parent;
        //    _destination = Destination;

        //    _clientIdleHandler = new ClientIdleHandler();
        //    _clientIdleHandler.Start();
        //    //start timer
        //    dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.Tick += TimerTick;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 10);
        //    dispatcherTimer.Start();
        //}

        //private void TimerTick(object sender, EventArgs e)
        //{
        //    if (_clientIdleHandler.IsActive)//active
        //    {
        //        //What you gonna do when idle

        //        _parent.Navigate(_destination);
        //    }
        //}
    }
}
