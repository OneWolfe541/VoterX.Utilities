using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VoterX.Core.Voters;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Views
{
    public class VoterSearchNameViewModel : NotifyPropertyChanged
    {
        public VoterSearchNameViewModel()
        {

        }

        public VoterSearchNameViewModel(VoterSearchModel SearchItems)
        {
            NameLast = SearchItems.LastName;
            NameFirst = SearchItems.FirstName;
            BirthYear = SearchItems.BirthYear;
        }

        private VoterSearchModel _voterSearch;
        public VoterSearchModel VoterSearch
        {
            get { return _voterSearch; }
            //set { _voterSearch = value; }
        }

        private void SetVoterSearch()
        {
            _voterSearch = new VoterSearchModel
            {
                FirstName = _nameFirst,
                LastName = _nameLast,
                BirthYear = _birthYear
            };
            RaisePropertyChanged("VoterSearch");
            //Console.WriteLine("Search Voter: " + _nameLast);
        }

        public bool VoterClear { get; set; }
        private void ClearVoterSearch()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
        }

        public bool VoterScan { get; set; }
        private void ScanVoterSearch()
        {
            _voterSearch = null;

            NameFirst = null;
            RaisePropertyChanged("NameFirst");
            NameLast = string.Empty;
            RaisePropertyChanged("NameLast");
            BirthYear = null;
            RaisePropertyChanged("BirthYear");

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterScan");
        }

        private string _nameLast;
        public string NameLast
        {
            get { return _nameLast; }
            set
            {
                _nameLast = value;
            }
        }

        private string _nameFirst;
        public string NameFirst
        {
            get { return _nameFirst; }
            set
            {
                _nameFirst = value;
            }
        }

        private string _birthYear;
        public string BirthYear
        {
            get { return _birthYear; }
            set
            {
                _birthYear = value;
            }
        }

        // Examples of how to get the button to work inside the ListBox
        // https://stackoverflow.com/questions/4500729/how-to-use-binding-in-the-listbox-s-items-to-the-viewmodel-s-properties
        // relayCommand from the John Smith articles
        // https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
        // https://stackoverflow.com/questions/51845335/how-to-bind-command-when-button-inside-of-listbox-item
        public RelayCommand _searchClickCommand;
        public ICommand SearchClickCommand
        {
            get
            {
                if (_searchClickCommand == null)
                {
                    _searchClickCommand = new RelayCommand(param => this.SetVoterSearch());
                }
                return _searchClickCommand;
            }
        }

        public RelayCommand _clearClickCommand;
        public ICommand ClearClickCommand
        {
            get
            {
                if (_clearClickCommand == null)
                {
                    _clearClickCommand = new RelayCommand(param => this.ClearVoterSearch());
                }
                return _clearClickCommand;
            }
        }

        public RelayCommand _scanClickCommand;
        public ICommand ScanClickCommand
        {
            get
            {
                if (_scanClickCommand == null)
                {
                    _scanClickCommand = new RelayCommand(param => this.ScanVoterSearch());
                }
                return _scanClickCommand;
            }
        }
    }
}
