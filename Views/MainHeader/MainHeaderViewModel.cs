using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Views
{
    public class MainHeaderViewModel : NotifyPropertyChanged
    {
        private Window _parent;

        public MainHeaderViewModel(Window mainWindow)
        {
            if (mainWindow != null)
            {
                _parent = mainWindow;
            }
            else
            {
                _parent = Application.Current.MainWindow;
            }
        }

        #region MessageProperties
        private string _pageHeader;
        public string PageHeader
        {
            get
            {
                return _pageHeader;
            }
            set
            {
                _pageHeader = value.ToUpper();
                RaisePropertyChanged("PageHeader");
            }
        }

        private bool _sampleModeVisibility;
        public bool SampleModeVisibility
        {
            get { return _sampleModeVisibility; }
            set
            {
                _sampleModeVisibility = value;
                RaisePropertyChanged("SampleModeVisibility");
            }
        }
        public bool SampleMode
        {
            get { return _sampleModeVisibility; }
            set
            {
                _sampleModeVisibility = value;
                RaisePropertyChanged("SampleModeVisibility");
            }
        }

        private bool _developmentModeVisibility;
        public bool DevelopmentModeVisibility
        {
            get { return _developmentModeVisibility; }
            set
            {
                _developmentModeVisibility = value;
                RaisePropertyChanged("DevelopmentModeVisibility");
            }
        }
        public bool DevMode
        {
            get { return _developmentModeVisibility; }
            set
            {
                _developmentModeVisibility = value;
                RaisePropertyChanged("DevelopmentModeVisibility");
            }
        }

        private bool _closeButtonVisibility;
        public bool CloseButtonVisibility
        {
            get { return _closeButtonVisibility; }
            set
            {
                _closeButtonVisibility = value;
                RaisePropertyChanged("CloseButtonVisibility");
            }
        }

        private bool _hamburgerMenuVisibility;
        public bool HamburgerMenuVisibility
        {
            get { return _hamburgerMenuVisibility; }
            set
            {
                _hamburgerMenuVisibility = value;
                CloseButtonVisibility = !value;
                RaisePropertyChanged("HamburgerMenuVisibility");
            }
        }

        public void RemoveHamburgerMenu()
        {
            _hamburgerMenuVisibility = false;
        }

        public bool MenuClicked { get; private set; }

        #endregion

        #region Commands
        // Bound command for returning to the search screen
        public RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => this.CloseClick());
                }
                return _closeCommand;
            }
        }

        // Force parent frame to navigate back to the search page
        public void CloseClick()
        {
            _parent.Close();
        }

        public RelayCommand _menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menuCommand == null)
                {
                    _menuCommand = new RelayCommand(param => this.MenuClick());
                }
                return _menuCommand;
            }
        }

        public void MenuClick()
        {
            MenuClicked = !MenuClicked;
            RaisePropertyChanged("MenuClicked");
        }
        #endregion
    }
}
