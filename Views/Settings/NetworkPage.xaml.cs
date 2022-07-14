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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoterX.Utilities.Methods;
using VoterX.Utilities.BasePageDefinitions;
//using VoterX.Core.Containers;
//using VoterX.Core.Repositories;
using VoterX.SystemSettings;
using VoterX.Utilities.UserControls;
using VoterX.Core.Elections;

namespace VoterX.Utilities.Views.Settings
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class NetworkPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public NetworkPage()
        {
            InitializeComponent();
        }

        public NetworkPage(NMElection election, SystemSettingsController settings, SettingStatus status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            DisplaySettings();

            pageLoaded = true;
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            _settings.Network.SQLServer = SQLServerName.Text;
            _settings.Network.LocalDatabase = DatabaseName.Text;

            _settings.Network.SQLMode = ComboBoxMethods.GetSelectedItemNumber(SQLModeList);

            // NETWORK SETTINGS //
            //_settings.Network.NetworkTypeID = Int32.Parse(NetworkType.Text);
            //_settings.Network.TableLink = TableLink.Text;
            //_settings.Network.StartDate = DateTime.Parse(StartDate.Text);
            //_settings.Network.LastLogin = DateTime.Parse(LastLogin.Text);
            //_settings.Network.LastEndOfDayUpdate = DateTime.Parse(LastEODUpdate.Text);
            //_settings.Network.LastSQLUpdate = DateTime.Parse(LastSQLUpdate.Text);
            //_settings.Network.LocalTable = LocalTable.Text;
            //_settings.Network.RemoteTable = RemoteTable.Text;
            //_settings.NetworkConfigs.Table2 = bool.Parse(Table2.Text);
            //_settings.Network.LocalTable2 = LocalTable2.Text;
            //_settings.Network.RemoteTable2 = RemoteTable2.Text;
        }        

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // NETWORK SETTINGS //

            try
            {
                SQLServerName.Text = _settings.Network.SQLServer.ToString();
            }
            catch { }

            try
            {
                DatabaseName.Text = _settings.Network.LocalDatabase.ToString();
            }
            catch { }

            try
            {
                SetSQLMode();
            }
            catch { }

            //NetworkType.Text = _settings.Network.NetworkTypeID.ToString();
            //TableLink.Text = _settings.Network.TableLink.ToString();
            //StartDate.Text = _settings.Network.StartDate.ToString();
            //LastLogin.Text = _settings.Network.LastLogin.ToString();
            //LastEODUpdate.Text = _settings.Network.LastEndOfDayUpdate.ToLocalTime().ToString();
            //LastSQLUpdate.Text = _settings.Network.LastSQLUpdate.ToLocalTime().ToString();
            //LocalTable.Text = _settings.Network.LocalTable.ToString();
            //RemoteTable.Text = _settings.Network.RemoteTable.ToString();
            //if (_settings.Network.Table2) Table2.Foreground = new SolidColorBrush(Colors.Green);
            //else Table2.Foreground = new SolidColorBrush(Colors.Red);
            //Table2.Text = _settings.Network.Table2.ToString();
            //LocalTable2.Text = _settings.Network.LocalTable2.ToString();
            //RemoteTable2.Text = _settings.Network.RemoteTable2.ToString();
        }

        private void SetSQLMode()
        {
            switch (_settings.Network.SQLMode)
            {
                case 0:
                    WindowsLogin.IsSelected = true;
                    break;
                case 1:
                    SQLLogin.IsSelected = true;
                    break;
            }
        }

        private void SettingsChange()
        {
            _settingsStatus.SettingsChanged = true;
        }

        private void SettingsChanged_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((ComboBox)sender).IsLoaded)
            {
                SettingsChange();
            }
        }

        private void SettingsChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                SettingsChange();
            }
        }
    }
}
