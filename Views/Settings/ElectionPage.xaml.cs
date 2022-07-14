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
//using VoterX.Core.Models.Database;
using VoterX.Utilities.UserControls;
using VoterX.Core.Elections;
using AutoVote.Logging;
using System.Collections.ObjectModel;

namespace VoterX.Utilities.Views.Settings
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class ElectionPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        private ObservableCollection<PartyModel> _partylist = null;

        public bool pageLoaded = false;
        private bool partyLoaded = false;

        public ElectionPage()
        {
            InitializeComponent();
        }

        public ElectionPage(NMElection election, SystemSettingsController settings, SettingStatus status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            LoadElectionListAsync();

            ShowPartyList();

            DisplaySettings();
        }

        private void ShowPartyList()
        {
            if (_settings.Election.ElectionType == SystemSettings.Enums.ElectionType.Primary)
            {
                LoadParties();

                PartiesBreak.Visibility = Visibility.Visible;
                PartiesLabel.Visibility = Visibility.Visible;
                PartiesList.Visibility = Visibility.Visible;
                //PartyLoadingPanel.Visibility = Visibility.Visible;
                ClearPartyButton.Visibility = Visibility.Visible;
            }
        }

        private void HidePartyList()
        {
            
            PartiesBreak.Visibility = Visibility.Collapsed;
            PartiesLabel.Visibility = Visibility.Collapsed;
            PartiesList.Visibility = Visibility.Collapsed;
            //PartyLoadingPanel.Visibility = Visibility.Visible;
            ClearPartyButton.Visibility = Visibility.Collapsed;
            
        }

        private async void LoadElectionListAsync()
        {
            // Create animated loading list item
            var loadingItem = ComboBoxMethods.AddLoadingItem(ElectionList, TempLoadingSpinnerItem);

            //int count = 0;
            if (_election != null && await Task.Run(() => _election.Exists()) == true)
            {
                //foreach (var electionItem in await Task.Run(() => _election.Lists.Election))
                {
                    var electionItem = _election.Lists.Election;

                    if(electionItem != null)
                    {
                        try
                        {
                            // Insert a single election item into the combobox
                            ComboBoxMethods.AddComboItemToControl(
                                ElectionList,                                                       // Destination Combobox
                                electionItem.ElectionId,                                            // Object added to DataContext
                                electionItem.ToString().TrimEnd(),                                  // String displayed in list
                                electionItem.ElectionId
                                );
                            // Increment Row Count
                            //count++;
                        }
                        catch (Exception e)
                        {
                            AutoVoteLogger _errorLogger = new AutoVoteLogger("VCCLogs", _settings.System.ReportErrorLogging); ;
                            _errorLogger.WriteLog("Election Settings: " + e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            // Try loading entire eleciton table instead
                            var electionList = _election.LoadAllElections();

                            foreach(var election in electionList)
                            {
                                ComboBoxMethods.AddComboItemToControl(
                                ElectionList,                                                       // Destination Combobox
                                election.ElectionId,                                            // Object added to DataContext
                                election.ToString().TrimEnd(),                                  // String displayed in list
                                election.ElectionId
                                );
                            }
                            if (electionList.Count() == 1)
                            {
                                ElectionList.SelectedIndex = 0;
                                PopulateElectionDetails(ElectionList);
                            }
                        }
                        catch (Exception e)
                        {
                            AutoVoteLogger _errorLogger = new AutoVoteLogger("VCCLogs", _settings.System.ReportErrorLogging); ;
                            _errorLogger.WriteLog("Election Settings: " + e.Message);
                        }
                    }                    
                }
            }

            // Remove animated loading list item
            ComboBoxMethods.RemoveListItem(ElectionList, loadingItem);
        }

        private void LoadElectionList()
        {
            ElectionList.Items.Clear();

            // Create animated loading list item
            var loadingItem = ComboBoxMethods.AddLoadingItem(ElectionList, TempLoadingSpinnerItem);

            //int count = 0;
            if (_election != null && _election.Exists() == true)
            {
                //foreach (var electionItem in await Task.Run(() => _election.Lists.Election))
                {
                    var electionItem = _election.Lists.Election;
                    if (electionItem != null)
                    {
                        try
                        {
                            // Insert a single election item into the combobox
                            ComboBoxMethods.AddComboItemToControl(
                                ElectionList,                                                       // Destination Combobox
                                electionItem.ElectionId,                                            // Object added to DataContext
                                electionItem.ToString().TrimEnd(),                                  // String displayed in list
                                electionItem.ElectionId
                                );
                            // Increment Row Count
                            //count++;
                        }
                        catch (Exception e)
                        {
                            AutoVoteLogger _errorLogger = new AutoVoteLogger("VCCLogs", _settings.System.ReportErrorLogging); ;
                            _errorLogger.WriteLog("Election Settings: " + e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            // Try loading entire eleciton table instead
                            var electionList = _election.LoadAllElections();

                            foreach (var election in electionList)
                            {
                                ComboBoxMethods.AddComboItemToControl(
                                ElectionList,                                                       // Destination Combobox
                                election.ElectionId,                                            // Object added to DataContext
                                election.ToString().TrimEnd(),                                  // String displayed in list
                                election.ElectionId
                                );

                                if (electionList.Count() == 1)
                                {
                                    ElectionList.SelectedIndex = 0;
                                    //PopulateElectionDetails(ElectionList);
                                    ElectionID.Text = election.ElectionId.ToString();
                                    CountyCode.Text = election.CountyCode;
                                    SetElectionType(ConvertElectionType(election.ElectionType));
                                    ElectionEntity.Text = election.CountyName;
                                    ElectionTitle.Text = election.ElectionName;
                                    if (election.ElectionDate != null)
                                    {
                                        ElectionDate.Text = ((DateTime)election.ElectionDate).ToShortDateString();
                                    }
                                }
                            }                            
                        }
                        catch (Exception e)
                        {
                            AutoVoteLogger _errorLogger = new AutoVoteLogger("VCCLogs", _settings.System.ReportErrorLogging); ;
                            _errorLogger.WriteLog("Election Settings: " + e.Message);
                        }
                    }
                }
            }

            // Remove animated loading list item
            ComboBoxMethods.RemoveListItem(ElectionList, loadingItem);
        }

        private void LoadParties()
        {
            if(_settings.Election.ElectionType == SystemSettings.Enums.ElectionType.Primary)
            {
                AutoVoteLogger settingsLog = new AutoVoteLogger("VCClogs", true);

                PartiesBreak.Visibility = Visibility.Visible;
                PartiesLabel.Visibility = Visibility.Visible;
                PartiesList.Visibility = Visibility.Visible;
                ClearPartyButton.Visibility = Visibility.Visible;

                if (_election.Lists.Partys != null)
                {
                    _partylist = new ObservableCollection<PartyModel>(_election.Lists.Partys);
                    PartiesList.ItemsSource = _partylist.OrderBy(o => o.PartyCode);
                }

                PartyLoadingPanel.Visibility = Visibility.Collapsed;

                // Disable item selection changed
                partyLoaded = false;

                try
                {
                    // Load previous selected list items
                    for (int i = 0; i < PartiesList.Items.Count; i++)
                    {
                        if (_settings.Election != null && _settings.Election.EligibleParties != null)
                        {
                            foreach (var party in _settings.Election.EligibleParties)
                            {
                                if (((PartyModel)PartiesList.Items[i]).PartyCode == party)
                                {
                                    PartiesList.SelectedItems.Add(PartiesList.Items[i]);
                                }
                            }
                        }
                        else
                        {
                            settingsLog.WriteLog("Load Party List Failed: Eligible Parties not found!");
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    settingsLog.WriteLog("Load Party List Failed: " + e.Message);
                    if (e.InnerException != null)
                    {
                        settingsLog.WriteLog(e.InnerException.Message);
                    }
                    //HidePartyList();
                    //return;
                }

                // Enable selection changed
                partyLoaded = true;
            }
        }

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            PopulateElectionDetails(ElectionList);

            //try
            //{
                //ELECTION SETTINGS //
                _settings.Election.ElectionID = Int32.Parse(ElectionID.Text);
                _settings.Election.CountyCode = CountyCode.Text;
                //_settings.Election.ElectionType = Int32.Parse(ElectionType.Text);
                _settings.Election.ElectionType = (SystemSettings.Enums.ElectionType)ComboBoxMethods.GetSelectedItemNumber(ElectionType);
                _settings.Election.ElectionEntity = ElectionEntity.Text;
                _settings.Election.ElectionTitle = ElectionTitle.Text;
                _settings.Election.ElectionDate = DateTime.Parse(ElectionDate.Text);

                //List<string> parties = new List<string>();
                //parties.Add("DEM");
                //parties.Add("REP");
                //parties.Add("LIB");
                //_settings.Election.EligibleParties = parties;
            //}
            //catch { }
        }

        //Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            //ELECTION SETTINGS //
            PopulateElectionDetails(ElectionList);
        }

        private void PopulateElectionDetails(object sender)
        {
            if (((ComboBox)sender).IsLoaded && _election != null)
            {
                int electionId = ComboBoxMethods.GetSelectedItemNumber(((ComboBox)sender));
                var selectedElection = _election.Lists.Election;

                if (selectedElection != null && selectedElection.ElectionId == electionId)
                {
                    ElectionID.Text = selectedElection.ElectionId.ToString();
                    CountyCode.Text = selectedElection.CountyCode;
                    SetElectionType(ConvertElectionType(selectedElection.ElectionType));
                    ElectionEntity.Text = selectedElection.CountyName;
                    ElectionTitle.Text = selectedElection.ElectionName;
                    if (selectedElection.ElectionDate != null)
                    {
                        ElectionDate.Text = ((DateTime)selectedElection.ElectionDate).ToShortDateString();
                    }
                }

                //StatusBar.ApplicationStatusCenter(selectedElection.election_name);
            }
        }

        private int ConvertElectionType(string value)
        {
            if (value == "Primary") return 1;
            else return 2;
        }

        private void SetElectionType(int type)
        {
            switch (type)
            {
                case 1:
                    PrimaryElectionType.IsSelected = true;
                    break;
                case 2:
                    GeneralElectionType.IsSelected = true;
                    break;
            }
        }

        private void ElectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateElectionDetails(sender);
        }

        private void ElectionList_DropDownOpened(object sender, EventArgs e)
        {
            LoadElectionList();
        }

        private void ElectionList_Loaded(object sender, RoutedEventArgs e)
        {
            DisplaySettings();
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

        private void PartiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedIndex >= 0 && partyLoaded != false)
            {
                if (_settings.Election.EligibleParties == null) _settings.Election.EligibleParties = new List<string>();

                _settings.Election.EligibleParties.Clear();

                var items = ((ListView)sender).SelectedItems;
                foreach (var item in items)
                {
                    PartyModel partyItem = item as PartyModel;
                    _settings.Election.EligibleParties.Add(partyItem.PartyCode);
                }
            }
        }

        private void ClearPartyButton_Click(object sender, RoutedEventArgs e)
        {
            _settings.Election.EligibleParties.Clear();
            PartiesList.SelectedItems.Clear();
        }
    }
}
