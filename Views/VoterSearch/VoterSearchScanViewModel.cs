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
    public class VoterSearchScanViewModel : NotifyPropertyChanged
    {
        public VoterSearchScanViewModel() : this(true) { }
        public VoterSearchScanViewModel(bool ShowButtons)
        {
            SearchVisibility = ShowButtons;
        }

        public bool SearchVisibility { get; set; }

        private VoterSearchModel _voterSearch;
        public VoterSearchModel VoterSearch
        {
            get { return _voterSearch; }
        }

        private void SetVoterSearch()
        {
            if (BarCode != null && BarCode != "")
            {
                _voterSearch = new VoterSearchModel
                {
                    VoterID = _barCode
                };
                RaisePropertyChanged("VoterSearch");

                LastBarCode = BarCode;
                BarCode = null;
                RaisePropertyChanged("LastBarCode");
                RaisePropertyChanged("BarCode");

                Console.WriteLine("Voter Id Entered: " + _voterSearch.VoterID);
            }
        }

        private bool _isBarCodeFocused;
        public bool IsBarcodeFocused
        {
            get
            {
                return _isBarCodeFocused;
            }

            set
            {
                _isBarCodeFocused = value;
                RaisePropertyChanged("IsBarcodeFocused");
            }
        }

        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set
            {
                _barCode = value;
                RaisePropertyChanged("BarCode");
            }
        }

        private string _lastBarCode;
        public string LastBarCode
        {
            get { return _lastBarCode; }
            set
            {
                _lastBarCode = value;
                RaisePropertyChanged("LastBarCode");
            }
        }

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

        public bool VoterClear { get; set; }
        private void ClearVoterSearch()
        {
            _voterSearch = null;

            BarCode = null;
            LastBarCode = null;

            RaisePropertyChanged("VoterClear");
        }

        public bool VoterSwitch { get; set; }
        private void SwitchVoterSearch()
        {
            _voterSearch = null;

            BarCode = null;
            LastBarCode = null;

            RaisePropertyChanged("VoterClear");
            RaisePropertyChanged("VoterSwitch");
        }

        public RelayCommand _switchClickCommand;
        public ICommand SwitchClickCommand
        {
            get
            {
                if (_switchClickCommand == null)
                {
                    _switchClickCommand = new RelayCommand(param => this.SwitchVoterSearch());
                }
                return _switchClickCommand;
            }
        }
    }
}
