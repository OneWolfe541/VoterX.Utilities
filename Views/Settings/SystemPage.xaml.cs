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
using VoterX.SystemSettings.Enums;

namespace VoterX.Utilities.Views.Settings
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class SystemPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public SystemPage()
        {
            InitializeComponent();
        }

        public SystemPage(NMElection election, SystemSettingsController settings)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            DisplaySettings();

            LoadPollSites();

            pageLoaded = true;
        }

        public SystemPage(NMElection election, SystemSettingsController settings, SettingStatus status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            DisplaySettings();

            LoadPollSites();

            LoadElectionDayPollName();

            pageLoaded = true;
        }

        public SystemPage(NMElection election, SystemSettingsController settings, bool status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = new SettingStatus { SettingsChanged = status };

            DisplaySettings();

            LoadPollSites();

            LoadElectionDayPollName();

            pageLoaded = true;
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            // SYSTEM SETTINGS //
            _settings.System.BODName = ApplicationName.Text;
            _settings.System.BODVersion = Version.Text;

            // Set Active site data
            var pollitem = ComboBoxMethods.GetSelectedItemNumber(PollList);
            if (pollitem > 0)
            {
                _settings.System.SiteID = ComboBoxMethods.GetSelectedItemNumber(PollList);
                _settings.System.SiteName = ComboBoxMethods.GetSelectedItem(PollList);
            }       

            _settings.System.MachineID = Int32.Parse(Machine.Text);
            _settings.System.VCCType = (VotingCenterMode)ComboBoxMethods.GetSelectedItemNumber(VCCTypeList);
            _settings.System.SearchOptions = (SearchMode)ComboBoxMethods.GetSelectedItemNumber(SearchOptions);
            _settings.System.LoginType = ComboBoxMethods.GetSelectedItemNumber(LoginTypeList);
            _settings.System.Permit = ComboBoxMethods.GetSelectedItemNumber(PrintPermit);
            _settings.System.BallotStub = ComboBoxMethods.GetSelectedItemNumber(PrintBallotStub);
            _settings.System.SignatureFolder = SignaturesFolder.Text;

            _settings.System.SiteVerified = (bool)VerifiedCheck.IsChecked;

            _settings.Reports.ReconcileCopies = Int32.Parse(ReconcileCopies.Text);
            _settings.System.IdRequired = (bool)IdRequiredCheck.IsChecked;
        }

        private async void LoadPollSites()
        {
            // Load Early Voting List
            var loadingItem = ComboBoxMethods.AddLoadingItem(PollList, TempLoadingSpinnerItem);

            if (_election != null && await Task.Run(() => _election.Exists()) == true)
            {
                foreach (var pollingLocation in await Task.Run(() => _election.Lists.Locations))
                {
                    ComboBoxMethods.AddComboItemToControl(
                        PollList,
                        pollingLocation.PollId,
                        pollingLocation.PlaceName,
                        (int)_settings.System.SiteID
                        );
                }
            }

            ComboBoxMethods.RemoveListItem(PollList, loadingItem);
        }

        private async void LoadElectionDayPollName()
        {
            if (_election != null && await Task.Run(() => _election.Exists()) == true)
            {
                try
                {
                    _settings.Site.ElectionDaySiteName = _election.Lists.Locations.Where(pol => pol.PollId == _settings.Site.ElectionDaySiteID).FirstOrDefault().PlaceName;
                }
                catch
                { }
            }
        }

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // SYSTEM SETTINGS //
            ApplicationName.Text = _settings.BODName.ToString();
            Version.Text = _settings.BODVersion.ToString();
            PollID.Text = _settings.System.SiteID.ToString();
            //PollName.Text = AppSettings.System.SiteName.ToString();
            Machine.Text = _settings.System.MachineID.ToString();

            //if (_settings.System.SiteVerified) Verified.Foreground = new SolidColorBrush(Colors.Green);
            //else Verified.Foreground = new SolidColorBrush(Colors.Red);
            //Verified.Text = _settings.System.SiteVerified.ToString();
            VerifiedCheck.IsChecked = _settings.System.SiteVerified;

            SignaturesFolder.Text = _settings.System.SignatureFolder;

            SetVCCType();

            SetLoginType();

            SetPermits();

            SetBallotStubs();

            SetSearchOptions();

            //HighlightActiveSite(_settings.System.VCCType);

            ReconcileCopies.Text = _settings.Reports.ReconcileCopies.ToString();
            IdRequiredCheck.IsChecked = _settings.System.IdRequired;
        }

        private void SetVCCType()
        {
            //VCCType.Text = AppSettings.System.VCCType.ToString();
            switch (_settings.System.VCCType)
            {
                case VotingCenterMode.EarlyVoting:
                    EarlyVoting.IsSelected = true;
                    break;
                case VotingCenterMode.ElectionDay:
                    ElectionDay.IsSelected = true;
                    break;
                case VotingCenterMode.SampleBallots:
                    SampleBallots.IsSelected = true;
                    break;
            }
        }

        private void SetLoginType()
        {
            //VCCType.Text = AppSettings.System.VCCType.ToString();
            switch (_settings.System.LoginType)
            {
                case 1:
                    SingleUser.IsSelected = true;
                    break;
                case 2:
                    MultiUser.IsSelected = true;
                    break;
            }
        }

        private void SetPermits()
        {
            //VCCType.Text = AppSettings.System.VCCType.ToString();
            switch (_settings.System.Permit)
            {
                case 0:
                    DontPrint.IsSelected = true;
                    break;
                case 1:
                    DoPrint.IsSelected = true;
                    break;
            }
        }

        private void SetBallotStubs()
        {
            //VCCType.Text = AppSettings.System.VCCType.ToString();
            switch (_settings.System.BallotStub)
            {
                case 0:
                    DontPrintStub.IsSelected = true;
                    break;
                case 1:
                    DoPrintStub.IsSelected = true;
                    break;
            }
        }

        private void SetSearchOptions()
        {
            //SearchOptions.Text = AppSettings.System.SearchOptions.ToString();
            switch (_settings.System.SearchOptions)
            {
                case SearchMode.Normal:
                    Normal.IsSelected = true;
                    break;
                case SearchMode.Scan:
                    Scan.IsSelected = true;
                    break;
                case SearchMode.Queue:
                    Queue.IsSelected = true;
                    break;
            }
        }

        private void PollList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Skip if the combobox isnt loaded
            if (((ComboBox)sender).IsLoaded)
            {
                PollID.Text = (ComboBoxMethods.GetSelectedItemData((ComboBox)sender).ToString());

                // Check if Early Voting mode is selected
                // Highlight Early Voting site
                //HighlightActiveSite(VCCTypeList.SelectedIndex + 1);
            }
        }

        private void FolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            // Mark settings have changed
            SettingsChange();

            SignaturesFolder.Text = OpenFolderBrowser(_settings.System.SignatureFolder);
        }

        // Folder Browser Sample
        // https://stackoverflow.com/questions/1922204/open-directory-dialog
        private string OpenFolderBrowser(string currentPath)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = currentPath;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                return dialog.SelectedPath;
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

        private void VerifiedCheck_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsLoaded)
            {
                SettingsChange();
            }
        }

        //private void EDPollList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Skip if the combobox isnt loaded
        //    if (((ComboBox)sender).IsLoaded)
        //    {
        //        var item = ComboBoxMethods.GetSelectedItemData((ComboBox)sender);
        //        EDPollID.Text = item == null ? "" : item.ToString();

        //        // Check if Election Day mode is selected
        //        // Highlight Election Day site
        //        HighlightActiveSite(VCCTypeList.SelectedIndex + 1);
        //    }
        //}

        private void VCCTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).IsLoaded)
            {
                //HighlightActiveSite(VCCTypeList.SelectedIndex + 1);
            }
        }

        private void ReconcileCopies_TextChanged(object sender, TextChangedEventArgs e)
        {
            int copies = 1;
            if(Int32.TryParse(ReconcileCopies.Text, out copies))
            {
                if (copies < 1) copies = 1;
                ReconcileCopies.Text = copies.ToString();
            }
            else
            {
                ReconcileCopies.Text = copies.ToString();
            }

            if (((TextBox)sender).IsLoaded)
            {
                SettingsChange();
            }
        }

        // https://stackoverflow.com/questions/660554/how-to-automatically-select-all-text-on-focus-in-wpf-textbox
        private void ReconcileCopies_GotFocus(object sender, RoutedEventArgs e)
        {
            ReconcileCopies.SelectAll();
        }

        //private void HighlightActiveSite(int mode)
        //{
        //    switch(mode)
        //        {
        //        case 1: // Early Voting Mode
        //        case 3: // Sample Ballot mode
        //            HighlightEarlyVotingSite();
        //            break;
        //        case 2: // Election Day Mode
        //            if (ComboBoxMethods.GetSelectedItemNumber(EDPollList) != 0)
        //            {
        //                HighlightElectionDaySite();
        //            }
        //            else
        //            {
        //                HighlightEarlyVotingSite();
        //            }
        //            break;
        //    }
        //}

        //private void HighlightEarlyVotingSite()
        //{
        //    EarlyVotingPollingPlaceLabelLight.Visibility = Visibility.Collapsed;
        //    EarlyVotingPollingPlaceLabelBold.Visibility = Visibility.Visible;
        //    ElectionDayPollingPlaceLabelLight.Visibility = Visibility.Visible;
        //    ElectionDayPollingPlaceLabelBold.Visibility = Visibility.Collapsed;

        //    EarlyVotingPollIDLabelLight.Visibility = Visibility.Collapsed;
        //    EarlyVotingPollIDLabelBold.Visibility = Visibility.Visible;
        //    ElectionDayPollIDLabelLight.Visibility = Visibility.Visible;
        //    ElectionDayPollIDLabelBold.Visibility = Visibility.Collapsed;
        //}

        //private void HighlightElectionDaySite()
        //{
        //    EarlyVotingPollingPlaceLabelLight.Visibility = Visibility.Visible;
        //    EarlyVotingPollingPlaceLabelBold.Visibility = Visibility.Collapsed;
        //    ElectionDayPollingPlaceLabelLight.Visibility = Visibility.Collapsed;
        //    ElectionDayPollingPlaceLabelBold.Visibility = Visibility.Visible;

        //    EarlyVotingPollIDLabelLight.Visibility = Visibility.Visible;
        //    EarlyVotingPollIDLabelBold.Visibility = Visibility.Collapsed;
        //    ElectionDayPollIDLabelLight.Visibility = Visibility.Collapsed;
        //    ElectionDayPollIDLabelBold.Visibility = Visibility.Visible;
        //}
    }
}
