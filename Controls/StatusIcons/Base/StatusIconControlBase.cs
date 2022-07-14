using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.Commands;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Controls
{
    public class StatusIconControlBase : NotifyPropertyChanged, IStatusIconControlBase
    {
        protected string DefaultIconName;
        protected string ControlName;

        public StatusIconMode CurrentStatus
        {
            get
            {
                if (_statusOkIcon == true)
                {
                    return StatusIconMode.Ready;
                }
                else if (_statusBadIcon == true)
                {
                    return StatusIconMode.NotReady;
                }
                else if (_statusSpinnerIcon == true)
                {
                    return StatusIconMode.Working;
                }
                else
                {
                    return StatusIconMode.Unknown;
                }
            }
        }

        #region ThreeIconStates
        private bool _statusOkIcon;
        protected bool StatusOkIcon
        {
            get { return _statusOkIcon; }
            set
            {
                _statusOkIcon = value;
                if (_statusOkIcon == true)
                {
                    IconName = DefaultIconName;
                    IconSpin = false;
                    IconColor = "ApplicationForegroundColor";
                    IconToolTip = ControlName + " Ready";
                    IconVisibility = true;
                    _statusBadIcon = false;
                    _statusSpinnerIcon = false;
                }
                else
                {
                    IconVisibility = false;
                }
                RaisePropertyChanged("StatusOkIcon");
                RaisePropertyChanged("CurrentStatus");
            }
        }

        private bool _statusBadIcon;
        protected bool StatusBadIcon
        {
            get { return _statusBadIcon; }
            set
            {
                _statusBadIcon = value;
                if (_statusBadIcon == true)
                {
                    IconName = DefaultIconName;
                    IconSpin = false;
                    IconColor = "ApplicationDangerColor";
                    IconToolTip = ControlName + " Not Ready";
                    IconVisibility = true;
                    _statusOkIcon = false;
                    _statusSpinnerIcon = false;
                }
                else
                {
                    IconVisibility = false;
                }
                RaisePropertyChanged("StatusBadIcon");
                RaisePropertyChanged("CurrentStatus");
            }
        }

        private bool _statusSpinnerIcon;
        protected bool StatusSpinnerIcon
        {
            get { return _statusSpinnerIcon; }
            set
            {
                _statusSpinnerIcon = value;
                if (_statusSpinnerIcon == true)
                {
                    IconName = "Spinner";
                    IconSpin = true;
                    IconColor = "ApplicationForegroundColor";
                    IconToolTip = "Checking " + ControlName + " Connection";
                    IconVisibility = true;
                    _statusOkIcon = false;
                    _statusBadIcon = false;
                }
                else
                {
                    IconVisibility = false;
                }
                RaisePropertyChanged("StatusSpinnerIcon");
                RaisePropertyChanged("CurrentStatus");
            }
        }
        #endregion

        #region InternalIconProperties
        private bool _iconVisibility;
        public bool IconVisibility
        {
            get { return _iconVisibility; }
            internal set
            {
                _iconVisibility = value;
                RaisePropertyChanged("IconVisibility");
            }
        }

        private string _iconName;
        public string IconName
        {
            get
            {
                if (_iconName == null || _iconName == "")
                {
                    _iconName = "Database";
                }
                return _iconName;
            }
            internal set
            {
                _iconName = value;
                RaisePropertyChanged("IconName");
            }
        }

        private bool _iconSpin;
        public bool IconSpin
        {
            get { return _iconSpin; }
            internal set
            {
                _iconSpin = value;
                RaisePropertyChanged("IconSpin");
            }
        }

        private string _iconColor;
        public string IconColor
        {
            get
            {
                if (_iconColor == null || _iconColor == "")
                {
                    _iconColor = "ApplicationForegroundColor";
                }
                return _iconColor;
            }
            internal set
            {
                _iconColor = value;
                RaisePropertyChanged("IconColor");
            }
        }

        private string _iconToolTip;
        public string IconToolTip
        {
            get
            {
                if (_iconToolTip == null || _iconToolTip == "")
                {
                    _iconToolTip = "Database Ready";
                }
                return _iconToolTip;
            }
            internal set
            {
                _iconToolTip = value;
                RaisePropertyChanged("IconToolTip");
            }
        }
        #endregion
    }
}
