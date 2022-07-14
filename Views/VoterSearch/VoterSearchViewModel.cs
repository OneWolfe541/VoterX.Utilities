using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoVote.Logging;
using VoterX.Core.Voters;
using VoterX.SystemSettings;
using VoterX.SystemSettings.Enums;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Views
{
    public class VoterSearchViewModel : NotifyPropertyChanged, IDisposable
    {
        public ObservableCollection<NMVoter> VoterList { get; set; }

        private NMVoter _selectedVoter;

        public bool PartyVisibility { get; set; }

        public VoterSearchViewModel(int? electionType)
        {
            // Hide Search Animation
            SearchAnimation = false;

            // Display voter's party for primary elections
            if (electionType == 1) PartyVisibility = true;
        }

        public VoterSearchViewModel(SystemSettingsController settings)
        {
            // Hide Search Animation
            SearchAnimation = false;

            // Display voter's party for primary elections
            if (settings.Election.ElectionType == ElectionType.Primary) PartyVisibility = true;            
        }

        #region LogCodeDescriptionColors
        private System.Windows.Media.Brush _logCodeValidColor;
        public System.Windows.Media.Brush LogCodeValidColor
        {
            get
            {
                if(_logCodeValidColor == null)
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

        //private Setter _logCodeValidSetter;
        //public Setter LogCodeValidSetter
        //{
        //    get
        //    {
        //        if(_logCodeValidSetter == null)
        //    }
        //}

        public void SetLogCodeColors(System.Windows.Media.Brush brush)
        {
            LogCodeValidColor = brush;
            RaisePropertyChanged("LogCodeValidColor");

            LogCodeDangerColor = brush;
            RaisePropertyChanged("LogCodeDangerColor");
        }
        #endregion

        public void DisplayResults(string results)
        {
            SearchResults = results;
        }

        public void LoadVoters(VoterSearchModel SearchItems, int? electionType)
        {
            VoterFactory voterFactory = new VoterFactory(electionType);

            VoterList = voterFactory.LimitedList(SearchItems);

            if (VoterList == null)
            {
                SearchResults = "Results: 0 Voters Found";
            }

            //if (electionType == 1 )
            //{
            //    PartyVisibility = true;
            //}
            //else
            //{
            //    PartyVisibility = false;
            //}
        }

        public async Task<bool> LoadVotersAsync(VoterSearchModel SearchItems, int? electionType)
        {
            VoterFactory voterFactory = new VoterFactory(electionType);

            VoterList = await Task.Run(() => voterFactory.PagedListAsync(SearchItems, 0, 50));

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                AutoVoteLogger searchLog = new AutoVoteLogger("VCClogs", true);
                searchLog.WriteLog("Results: " + VoterList.Count());

                SearchListVisibility = true;
            }

            return true;
        }

        public async Task<bool> LoadProvisionalVotersAsync(VoterSearchModel SearchItems, int PollId, int? electionType)
        {
            VoterFactory voterFactory = new VoterFactory(electionType);

            VoterList = await Task.Run(() => voterFactory.ProvisionalOrderedListAsync(SearchItems, PollId));

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                AutoVoteLogger searchLog = new AutoVoteLogger("VCClogs", true);
                searchLog.WriteLog("Results: " + VoterList.Count());

                SearchListVisibility = true;
            }

            return true;
        }

        // Dynamic function for loading voter lists from a query function
        // https://stackoverflow.com/questions/8099631/how-to-return-value-from-action
        public async Task<bool> LoadVotersAsync(Func<ObservableCollection<NMVoter>> action)
        {
            VoterList = null;
            VoterList = await Task.Run(() => action());            

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                AutoVoteLogger searchLog = new AutoVoteLogger("VCClogs", true);
                searchLog.WriteLog("Results: " + VoterList.Count());

                if (VoterList.FirstOrDefault().Error != null)
                {
                    //SearchResults = VoterList.FirstOrDefault().Error.Message;
                    throw VoterList.FirstOrDefault().Error;
                    //return false;
                }
                else
                {
                    SearchListVisibility = true;

                    //long size = 0;
                    //object obj = VoterList;
                    //using (Stream s = new MemoryStream())
                    //{
                    //    BinaryFormatter formatter = new BinaryFormatter();
                    //    formatter.Serialize(s, obj);
                    //    size = s.Length;
                    //}
                    //Console.WriteLine("List Size: " + size.ToString());
                }
            }

            return true;
        }

        // Dynamic loading function for ASYNC query functions
        public async Task<bool> LoadVotersAsync(Func<Task<ObservableCollection<NMVoter>>> action)
        {
            VoterList = null;
            VoterList = await Task.Run(() => action());

            if (VoterList == null || VoterList.Count() <= 0)
            {
                SearchResults = "Results: 0 Voters Found";
            }
            else
            {
                AutoVoteLogger searchLog = new AutoVoteLogger("VCClogs", true);
                searchLog.WriteLog("Results: " + VoterList.Count());

                if (VoterList.FirstOrDefault().Error != null)
                {
                    //SearchResults = VoterList.FirstOrDefault().Error.Message;
                    throw VoterList.FirstOrDefault().Error;
                    //return false;
                }
                else
                {
                    SearchListVisibility = true;

                    //long size = 0;
                    //object obj = VoterList;
                    //using (Stream s = new MemoryStream())
                    //{
                    //    BinaryFormatter formatter = new BinaryFormatter();
                    //    formatter.Serialize(s, obj);
                    //    size = s.Length;
                    //}
                    //Console.WriteLine("List Size: " + size.ToString());
                }
            }

            return true;
        }

        public NMVoter SelectedVoter
        {
            get
            {
                return _selectedVoter; 
            }

            set
            {
                _selectedVoter = value;
            }
        }

        // Examples of how to get the button to work inside the ListBox
        // https://stackoverflow.com/questions/4500729/how-to-use-binding-in-the-listbox-s-items-to-the-viewmodel-s-properties
        // relayCommand from the John Smith articles
        // https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
        // https://stackoverflow.com/questions/51845335/how-to-bind-command-when-button-inside-of-listbox-item
        public RelayCommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand == null)
                {
                    _clickCommand = new RelayCommand(param => this.VoterClick(param));
                }
                return _clickCommand;
            }
        }

        public void VoterClick(object param)
        {
            _selectedVoter = (NMVoter)param;
            RaisePropertyChanged("SelectedVoter");
        }

        public void Dispose()
        {
            VoterList = null;
            _selectedVoter = null;

            GC.Collect();
        }

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
    }
}
