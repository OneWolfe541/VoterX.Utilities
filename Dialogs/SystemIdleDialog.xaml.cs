using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using FontAwesome.WPF;

namespace VoterX.Utilities.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class SystemIdleDialog : Window
    {
        private DispatcherTimer _timer;
        private System.Diagnostics.Stopwatch _watch;

        public SystemIdleDialog()
        {
            InitializeComponent();
            //lblmessage.Content = message;

            lblmessage.Text = "AutoVote is idle and will automaticly log out in 20 seconds. \r\nClick Continue if you would like to keep using the system.";

            //this.Icon = FontAwesomeIcon.Exclamation;
            // Get icon images from https://paulferrett.com/fontawesome-favicon/
            Uri iconUri = new Uri("c://Program Files//AutoVote/Images/favicon-exclamation.ico");
            this.Icon = BitmapFrame.Create(iconUri);

            _watch = System.Diagnostics.Stopwatch.StartNew();

            _timer = new DispatcherTimer
                (
                TimeSpan.FromSeconds(1),
                DispatcherPriority.Background,
                (s, e) => 
                {                    
                    DisplayLogOutMessage();                    
                },
                Application.Current.Dispatcher
                );
        }

        private void DisplayLogOutMessage()
        {
            if (_watch != null && Int32.TryParse(_watch.Elapsed.ToString(@"ss"), out int timeRemaining))
            {
                if(timeRemaining < 21)
                {
                    lblmessage.Text = "AutoVote is idle and will automaticly log out in " + (20 - timeRemaining).ToString() + " seconds. \r\nClick Continue if you would like to keep using the system.";
                }
                else
                {
                    _timer.Stop();
                    _timer.IsEnabled = false;
                    _watch = null;
                    _timer = null;
                    this.DialogResult = false;
                    //this.Close();
                }
            }
            else
            {
                _timer.Stop();
                _timer.IsEnabled = false;
                _watch = null;
                _timer = null;
                this.DialogResult = false;
                //this.Close();
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.IsEnabled = false;
            _timer = null;
            _watch = null;
            this.DialogResult = true;
        }
    }
}
