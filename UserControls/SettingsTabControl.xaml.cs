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
using VoterX.Core.Elections;
//using VoterX.Core.Containers;
using VoterX.SystemSettings;
using VoterX.Utilities.BasePageDefinitions;

namespace VoterX.Utilities.UserControls
{
    public class SettingStatus
    {
        public bool SettingsChanged { get; set; }

        public SettingStatus()
        {
            SettingsChanged = false;
        }
    }
    /// <summary>
    /// Interaction logic for SettingsTabControl.xaml
    /// </summary>
    public partial class SettingsTabControl : UserControl
    {
        public NMElection Election { get; set; }

        public SystemSettingsController Settings { get; set; }

        public SettingStatus Status = new SettingStatus();

        public Visibility SystemSettingsTab
        {
            get
            {
                return SystemTab.Visibility;
            }
            set
            {
                SystemTab.Visibility = value;
            }
        }

        public Visibility NetworkSettingsTab
        {
            get
            {
                return NetworkTab.Visibility;
            }
            set
            {
                NetworkTab.Visibility = value;
            }
        }

        public Visibility PrinterSettingsTab
        {
            get
            {
                return PrintersTab.Visibility;
            }
            set
            {
                PrintersTab.Visibility = value;
            }
        }

        public Visibility ElectionSettingsTab
        {
            get
            {
                return ElectionTab.Visibility;
            }
            set
            {
                ElectionTab.Visibility = value;
            }
        }

        public Visibility BallotSettingsTab
        {
            get
            {
                return BallotsTab.Visibility;
            }
            set
            {
                BallotsTab.Visibility = value;
            }
        }

        public Visibility TabulatorSettingsTab
        {
            get
            {
                return TabulatorsTab.Visibility;
            }
            set
            {
                TabulatorsTab.Visibility = value;
            }
        }

        public Visibility UserSettingsTab
        {
            get
            {
                return UserTab.Visibility;
            }
            set
            {
                UserTab.Visibility = value;
            }
        }

        public Visibility ServersSettingsTab
        {
            get
            {
                return ServersTab.Visibility;
            }
            set
            {
                ServersTab.Visibility = value;
            }
        }

        public SettingsTabControl()
        {
            InitializeComponent();
        }

        public void LoadFirstPage()
        {
            string tabname = SelectFirstTab();
            //Dispatcher.BeginInvoke((Action)(() => SelectionTabControl.SelectedIndex = 2));
            SettingsFrame.Navigate(new Views.Settings.SystemPage(Election, Settings, Status));
        }

        // Highlights the first visible tab
        private string SelectFirstTab()
        {
            // Loop through all tabs and select the first one set to visible
            int i = 0;
            // https://www.codeproject.com/Questions/568247/Howplustoplusremoveplusallplustabplusitemsplusexce
            foreach (TabItem tabItem in SelectionTabControl.Items)
            {
                if (tabItem.Visibility == Visibility.Visible)
                {
                    Dispatcher.BeginInvoke((Action)(() => SelectionTabControl.SelectedIndex = i));
                    return tabItem.Header.ToString();
                }
                i++;
            }
            return null;
        }

        // Save changes made on the current page to the settings file
        public void Savechanges()
        {
            var settingsPage = (SettingsBasePage)SettingsFrame.Content;         // Get active page
            settingsPage.SaveSettings();                                        // Copy settings from page to global object

            Settings.SaveSettings();                                            // Save settings to the file
        }

        // Remember changes when switching from one tab to the next
        private void SaveLocalChanges()
        {
            var settingsPage = (SettingsBasePage)SettingsFrame.Content;         // Get active page
            if (settingsPage != null)
            {
                settingsPage.SaveSettings();                                        // Copy settings from page to global object
            }
        }

        private void SystemTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.SystemPage(Election, Settings, Status));
        }

        public void NavigateToSystemTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.SystemPage(Election, Settings, Status));
        }

        private void NetworkTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.NetworkPage(Election, Settings, Status));
        }

        public void NavigateToNetworkTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.NetworkPage(Election, Settings, Status));
        }

        private void PrintersTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.PrintersPage(Election, Settings, SettingsFrame, Status));
        }

        public void NavigateToPrintersTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.PrintersPage(Election, Settings, SettingsFrame, Status));
        }

        private void ElectionTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.ElectionPage(Election, Settings, Status));
        }

        public void NavigateToElectionTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.ElectionPage(Election, Settings, Status));
        }

        private void BallotsTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.BallotsPage(Election, Settings, Status));
        }

        public void NavigateToBallotsTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.BallotsPage(Election, Settings, Status));
        }

        private void UserTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.UserPage(Election, Settings, Status));
        }

        private void ServersTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.ServersPage(Election, Settings, Status));
        }

        private void SelectionTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SaveLocalChanges();
            //SettingsFrame.Navigate(new Views.Settings.NetworkPage(Election, Settings, Status));
        }

        private void TabulatorsTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.TabulatorsPage(Election, Settings, Status));
        }

        public void NavigateToTabulatorsTab()
        {
            SaveLocalChanges();
            SettingsFrame.Navigate(new Views.Settings.TabulatorsPage(Election, Settings, Status));
        }
    }
}
