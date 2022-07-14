using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Controls
{
    public class YesNoQuestionViewModel : NotifyPropertyChanged
    {
        public YesNoQuestionViewModel(string Question)
        {
            DisplayQuestion = Question;
            CanClickYes = true;
            CanClickNo = true;
        }

        public string DisplayQuestion { get; set; }

        public bool YesChecked { get; private set; }
        public bool NoChecked { get; private set; }

        public bool? IsChecked
        {
            get
            {
                if (YesChecked == true)
                {
                    return true;
                }
                else if (NoChecked == true)
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DisableOnClick { get; set; }

        public void YesClicked()
        {
            YesChecked = true;
            RaisePropertyChanged("YesChecked");
            NoChecked = false;
            RaisePropertyChanged("NoChecked");
            CanClickYes = true;
            RaisePropertyChanged("CanClickYes");
            if (DisableOnClick == true)
            {
                CanClickNo = false;
                RaisePropertyChanged("CanClickNo");
                CanClickYes = false;
                RaisePropertyChanged("CanClickYes");
            }
            RaisePropertyChanged("IsChecked");
        }

        public RelayCommand _yesClickCommand;
        public ICommand YesClickCommand
        {
            get
            {
                if (_yesClickCommand == null)
                {
                    _yesClickCommand = new RelayCommand(param => this.YesClicked(), param => this.CanClickYes);
                }
                return _yesClickCommand;
            }
        }

        private bool _canClickYes;
        public bool CanClickYes
        {
            get { return _canClickYes; }
            internal set
            {
                _canClickYes = value;
                RaisePropertyChanged("CanClickYes");
            }
        }

        public void NoClicked()
        {
            YesChecked = false;
            RaisePropertyChanged("YesChecked");
            NoChecked = true;
            RaisePropertyChanged("NoChecked");
            CanClickNo = true;
            RaisePropertyChanged("CanClickNo");
            if (DisableOnClick == true)
            {
                CanClickYes = false;
                RaisePropertyChanged("CanClickYes");
                CanClickNo = false;
                RaisePropertyChanged("CanClickNo");
            }
            RaisePropertyChanged("IsChecked");
        }

        public RelayCommand _noClickCommand;
        public ICommand NoClickCommand
        {
            get
            {
                if (_noClickCommand == null)
                {
                    _noClickCommand = new RelayCommand(param => this.NoClicked(), param => this.CanClickNo);
                }
                return _noClickCommand;
            }
        }

        private bool _canClickNo;
        public bool CanClickNo
        {
            get { return _canClickNo; }
            internal set
            {
                _canClickNo = value;
                RaisePropertyChanged("CanClickNo");
            }
        }
    }
}
