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
using VoterX.SystemSettings;
using VoterX.SystemSettings.Models;

namespace VoterX.Utilities.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class ValidationDialog : Window
    {
        string userName;
        UserSettingsModel userSettings;

        public ValidationDialog(UserSettingsModel user, string username)
        {
            InitializeComponent();

            userName = username;

            userSettings = user;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (userName == "Administrator" && txtAnswer.Password == userSettings.AdminPassword)
            {
                this.DialogResult = true;
            }

            else if (userName == "Manager" && txtAnswer.Password == userSettings.ManagePassword)
            {
                this.DialogResult = true;
            }

            else if (userName == "SuperUser" && txtAnswer.Password == userSettings.SuperPassword)
            {
                this.DialogResult = true;
            }

            else
            {
                this.DialogResult = false;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();

            Keyboard.Focus(txtAnswer);
        }
    }
}
