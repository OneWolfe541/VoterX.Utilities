using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Voters;
using VoterX.SystemSettings;
using VoterX.SystemSettings.Enums;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Views
{
    public class VoterListViewModel : NotifyPropertyChanged
    {
        public bool PartyVisibility { get; set; }

        public VoterListViewModel(int? electionType)
        {
            // Hide Search Animation
            SearchAnimation = false;

            // Display voter's party for primary elections
            if (electionType == 1) PartyVisibility = true;
        }

        public VoterListViewModel(SystemSettingsController settings)
        {
            // Hide Search Animation
            SearchAnimation = false;

            // Display voter's party for primary elections
            if (settings.Election.ElectionType == ElectionType.Primary) PartyVisibility = true;
        }

        private ObservableCollection<NMVoter> _voterList;
        public ObservableCollection<NMVoter> VoterList
        {
            get
            {
                if(_voterList == null)
                {
                    _voterList = new ObservableCollection<NMVoter>();
                }
                return _voterList;
            }
            set
            {
                _voterList = value;
            }
        }

        #region VoterCommands
        public void AddVoter(NMVoter voterToAdd)
        {
            VoterList.Add(voterToAdd);
            RaisePropertyChanged("VoterList");

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                SearchListVisibility = true;
                RaisePropertyChanged("SearchListVisibility");
            }
        }

        public void AddVoter(ObservableCollection<NMVoter> votersToAdd)
        {
            // Get new list from combined lists
            VoterList = VoterList.CombineLists(votersToAdd);
            RaisePropertyChanged("VoterList");

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                SearchListVisibility = true;
                RaisePropertyChanged("SearchListVisibility");
            }
        }

        // Dynamic function for loading voter lists from a query function
        // https://stackoverflow.com/questions/8099631/how-to-return-value-from-action
        public async void AddVoter(Func<ObservableCollection<NMVoter>> action)
        {
            // Get new list from combined lists
            VoterList = VoterList.CombineLists(await Task.Run(() => action()));
            RaisePropertyChanged("VoterList");

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                SearchListVisibility = true;
                RaisePropertyChanged("SearchListVisibility");
            }
        }

        public void RemoveSelectedVoter()
        {
            if(SelectedVoter != null)
            {
                RemoveVoter(SelectedVoter);
            }
        }

        public void RemoveVoter(NMVoter voter)
        {
            VoterList.Remove(voter);
            RaisePropertyChanged("VoterList");
        }

        public void ClearList()
        {
            VoterList = new ObservableCollection<NMVoter>();
            RaisePropertyChanged("VoterList");
        }

        private NMVoter _selectedVoter;
        public NMVoter SelectedVoter
        {
            get
            {
                return _selectedVoter;
            }

            set
            {
                _selectedVoter = value;
                RaisePropertyChanged("SelectedVoter");
            }
        }

        public bool IdExists(string voterId)
        {
            bool result = false;
            foreach (var v in VoterList)
            {
                if (v.Data.VoterID == voterId) return true;
            }
            return result;
        }
        #endregion

        #region LogCodeDisplay
        private bool? _logStatusVisibility;
        public bool LogStatusVisibility
        {
            get
            {
                if(_logStatusVisibility == null)
                {
                    _logStatusVisibility = true;
                }
                return (bool)_logStatusVisibility;
            }

            set
            {
                _logStatusVisibility = value;
                _ballotStyleVisibility = !_logStatusVisibility;
            }
        }

        private bool? _ballotStyleVisibility;
        public bool BallotStyleVisibility
        {
            get
            {
                if(_ballotStyleVisibility == null)
                {
                    _ballotStyleVisibility = false;
                }
                return (bool)_ballotStyleVisibility;
            }

            set
            {
                _ballotStyleVisibility = value;
                _logStatusVisibility = !_ballotStyleVisibility;
            }
        }

        private System.Windows.Media.Brush _logCodeValidColor;
        public System.Windows.Media.Brush LogCodeValidColor
        {
            get
            {
                if (_logCodeValidColor == null)
                {
                    _logCodeValidColor = System.Windows.Media.Brushes.Green;
                }
                return _logCodeValidColor;
            }

            set
            {
                _logCodeValidColor = value;
                RaisePropertyChanged("LogCodeValidColor");
            }
        }

        private System.Windows.Media.Brush _logCodeDangerColor;
        public System.Windows.Media.Brush LogCodeDangerColor
        {
            get
            {
                if (_logCodeDangerColor == null)
                {
                    _logCodeDangerColor = System.Windows.Media.Brushes.Red;
                }
                return _logCodeDangerColor;
            }

            set
            {
                _logCodeDangerColor = value;
                RaisePropertyChanged("LogCodeDangerColor");
            }
        }

        public void SetLogCodeColors(System.Windows.Media.Brush brush)
        {
            LogCodeValidColor = brush;
            RaisePropertyChanged("LogCodeValidColor");

            LogCodeDangerColor = brush;
            RaisePropertyChanged("LogCodeDangerColor");
        }
        #endregion

        #region Animation
        private bool _searchAnimation = false;
        public bool SearchAnimation
        {
            get { return _searchAnimation; }
            set
            {
                _searchAnimation = value;
                RaisePropertyChanged("SearchAnimation");
                SearchResultsVisibility = false;
                RaisePropertyChanged("SearchResultsVisibility");
            }
        }

        private bool _searchListVisibility;
        public bool SearchListVisibility
        {
            get { return _searchListVisibility; }
            set
            {
                _searchListVisibility = value;
                RaisePropertyChanged("SearchListVisibility");
            }
        }

        private bool _searchResultsVisibility;
        public bool SearchResultsVisibility
        {
            get { return _searchResultsVisibility; }
            set
            {
                _searchResultsVisibility = value;
                RaisePropertyChanged("SearchResultsVisibility");
            }
        }

        private string _searchResults;
        public string SearchResults
        {
            get { return _searchResults; }
            set
            {
                SearchResultsVisibility = true;
                RaisePropertyChanged("SearchResultsVisibility");
                _searchResults = value;
                RaisePropertyChanged("SearchResults");
            }
        }
        #endregion
    }
}
