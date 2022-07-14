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
using VoterX.Utilities.Extensions;
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
    public partial class ServersPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public ServersPage()
        {
            InitializeComponent();
        }

        public ServersPage(NMElection election, SystemSettingsController settings, SettingStatus status)
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
            // SERVER SETTINGS //
            if (ServerList.SelectedIndex >= 0)
            {
                CurrentServer.Text = ServerList.GetSelectedItem();
                _settings.Network.SQLServer = CurrentServer.Text;
            }

            if (DatabaseList.SelectedIndex >= 0)
            {
                CurrentDatabase2.Text = DatabaseList.GetSelectedItem();
                _settings.Network.LocalDatabase = CurrentDatabase2.Text;
            }
        }        

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // SERVER SETTINGS //
            CurrentServer.Text = _settings.Network.SQLServer;
            CurrentDatabase2.Text = _settings.Network.LocalDatabase;
        }

        public async Task<bool> LoadServersList()
        {
            var serverList = await SQLConnectionFinder.GetServerListAsync();

            ServerList.Items.Clear();

            foreach (string serverName in serverList)
            {
                AddComboItemToControl(ServerList, serverName, "AESSQL2");
            }

            return true;
        }

        // Add a single item to a combo box
        private void AddComboItemToControl(object sender, string newItem, string selectedItem)
        {
            // Create blank list item
            ComboBoxItem item = new ComboBoxItem();
            // Set list item name
            item.Content = newItem.ToUpper();
            // Check default for null and empty strings
            if (selectedItem != null && selectedItem != "" && selectedItem.Replace(" ", "") != "")
            {
                // Set selected item default from given string
                if (newItem.ToUpper().Contains(selectedItem.ToUpper()))
                    item.IsSelected = true;
            }
            // Add the item to the combo box
            ((ComboBox)sender).Items.Add(item);
        }

        private void ServerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentServer.Text = ServerList.GetSelectedItem();
        }

        private void ServerList_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void ServerList_DropDownOpened(object sender, EventArgs e)
        {
            if (ServerList.Items.Count == 1)
            {                
                var done = await LoadServersList();
            }
        }

        private void DatabaseList_DropDownOpened(object sender, EventArgs e)
        {
            if (ServerList.SelectedIndex >= 0)
            {
                DatabaseList.Items.Clear();

                string server = ServerList.GetSelectedItem();

                var dbList = SQLConnectionFinder.GetDataBases(server);

                foreach (string dbName in dbList)
                {
                    AddComboItemToControl(DatabaseList, dbName, _settings.Network.LocalDatabase);
                }
            }
        }

        private void DatabaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDatabase2.Text = DatabaseList.GetSelectedItem();
        }

        private void DatabaseList_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
