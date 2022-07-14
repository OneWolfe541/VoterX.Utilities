using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.SystemSettings.Enums;
using VoterX.SystemSettings.Models;
using VoterX.Utilities.Commands;
using VoterX.Utilities.Controls;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Views
{
    public class StatusBarViewModel : NotifyPropertyChanged
    {
        private readonly string _connection;

        public StatusBarViewModel(string ConnectionString)
        {
            _connection = ConnectionString;
        }

        #region MessageProperties
        private string _textRight;
        public string TextRight
        {
            get
            {
                return _textRight;
            }
            set
            {
                _textRight = value;
                RaisePropertyChanged("TextRight");
            }
        }

        private string _textLeft;
        public string TextLeft
        {
            get
            {
                return _textLeft;
            }
            set
            {
                _textLeft = value;
                RaisePropertyChanged("TextLeft");
            }
        }

        private string _textCenter;
        public string TextCenter
        {
            get
            {
                return _textCenter;
            }
            set
            {
                _textCenter = value;
                RaisePropertyChanged("TextCenter");
            }
        }

        public void TextClear()
        {
            TextRight = "";
            TextLeft = "";
            TextCenter = "";
        }

        private bool _spinnerVisibility;
        public bool SpinnerVisibility
        {
            get { return _spinnerVisibility; }
            set
            {
                _spinnerVisibility = value;
                RaisePropertyChanged("SpinnerVisibility");
            }
        }

        private string _systemMode;
        public string SystemMode
        {
            get { return _systemMode; }
            internal set
            {
                _systemMode = value;
                RaisePropertyChanged("SystemMode");
            }
        }

        private string _systemModeToolTip;
        public string SystemModeToolTip
        {
            get { return _systemModeToolTip; }
            internal set
            {
                _systemModeToolTip = value;
                RaisePropertyChanged("SystemModeToolTip");
            }
        }
        #endregion


        #region SystemMode
        public void DisplaySystemMode(int Mode)
        {
            switch (Mode)
            {
                case 1:
                    SystemMode = "EV";
                    SystemModeToolTip = "Early Voting";
                    break;
                case 2:
                    SystemMode = "ED";
                    SystemModeToolTip = "Election Day";
                    break;
                case 3:
                    SystemMode = "SB";
                    SystemModeToolTip = "Sample Ballots";
                    break;
                case 4:
                    SystemMode = "ABS";
                    SystemModeToolTip = "Absentee";
                    break;
            }
        }

        public void DisplaySystemMode(VotingCenterMode Mode)
        {
            switch (Mode)
            {
                case VotingCenterMode.EarlyVoting:
                    //SystemMode = "EV";
                    //SystemModeToolTip = "Early Voting";
                    DisplaySystemMode(1);
                    break;
                case VotingCenterMode.ElectionDay:
                    //SystemMode = "ED";
                    //SystemModeToolTip = "Election Day";
                    DisplaySystemMode(2);
                    break;
                case VotingCenterMode.SampleBallots:
                    //SystemMode = "SB";
                    //SystemModeToolTip = "Sample Ballots";
                    DisplaySystemMode(3);
                    break;
                case VotingCenterMode.Absentee:
                    //SystemMode = "ABS";
                    //SystemModeToolTip = "Absentee";
                    DisplaySystemMode(4);
                    break;
            }
        }

        //public void DisplaySystemMode(SystemMode Mode)
        //{
        //    switch (Mode)
        //    {
        //        case SystemSettings.Enums.SystemMode.EarlyVoting:
        //            //SystemMode = "EV";
        //            //SystemModeToolTip = "Early Voting";
        //            DisplaySystemMode(1);
        //            break;
        //        case SystemSettings.Enums.SystemMode.ElectionDay:
        //            //SystemMode = "ED";
        //            //SystemModeToolTip = "Election Day";
        //            DisplaySystemMode(2);
        //            break;
        //        case SystemSettings.Enums.SystemMode.SampleBallots:
        //            //SystemMode = "SB";
        //            //SystemModeToolTip = "Sample Ballots";
        //            DisplaySystemMode(3);
        //            break;
        //        case SystemSettings.Enums.SystemMode.Absentee:
        //            //SystemMode = "ABS";
        //            //SystemModeToolTip = "Absentee";
        //            DisplaySystemMode(4);
        //            break;
        //    }
        //}
        #endregion


        #region DatabaseStatus
        private StatusIconMode _databaseStatus;
        public StatusIconMode DatabaseStatus
        {
            get
            {
                return _databaseStatus;
            }
            private set
            {
                _databaseStatus = value;
                RaisePropertyChanged("DatabaseStatus");
            }
        }

        private DatabaseStatusViewModel _databaseStatusModel;
        internal DatabaseStatusViewModel DatabaseStatusModel
        {
            get
            {
                if (_databaseStatusModel is null)
                {
                    // Create new Database Status Model
                    _databaseStatusModel = new DatabaseStatusViewModel(_connection, 1000);
                    // Add Property Changed event handler
                    _databaseStatusModel.PropertyChanged += OnDatabaseStatusChanged;
                    return _databaseStatusModel;
                }
                else
                {
                    return _databaseStatusModel;
                }
            }
            set
            {
                _databaseStatusModel = value;
                RaisePropertyChanged("DatabaseStatusModel");
            }
        }

        public void CheckDatabaseStatus(int? WaitTime)
        {
            DatabaseStatusModel.CheckConnection(WaitTime);

            //TextCenter = DatabaseStatusModel.CurrentStatus.ToString();
        }

        public async Task<bool> CheckDatabaseStatusAsync(int? WaitTime)
        {
            bool result = await DatabaseStatusModel.CheckConnectionAsync(WaitTime);

            return result;
        }

        public async Task<bool> CheckDatabaseStatusAsync(int? WaitTime, string connection)
        {
            bool result = await DatabaseStatusModel.CheckConnectionAsync(WaitTime, connection);

            return result;
        }

        private bool _databaseError;
        public bool DatabaseError
        {
            get { return _databaseError; }
            private set
            {
                _databaseError = value;
                RaisePropertyChanged("DatabaseError");
            }
        }

        private void OnDatabaseStatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentStatus")
            {
                //Console.WriteLine(
                //    "Current Database Status: " +
                //    ((DatabaseStatusViewModel)sender).CurrentStatus.ToString()
                //    );

                DatabaseStatus = ((DatabaseStatusViewModel)sender).CurrentStatus;

                if (DatabaseStatus == StatusIconMode.NotReady)
                {
                    // Display Warning Message
                    TextCenter = "Database Not Found";
                    Console.WriteLine("Database Not Found");
                }
            }
        }
        #endregion


        #region PrinterStatus
        private StatusIconMode _printerStatus;
        public StatusIconMode PrinterStatus
        {
            get
            {
                return _printerStatus;
            }
            private set
            {
                _printerStatus = value;
                RaisePropertyChanged("PrinterStatus");
            }
        }

        private PrinterStatusViewModel _printerStatusModel;
        internal PrinterStatusViewModel PrinterStatusModel
        {
            get
            {
                if (_printerStatusModel is null)
                {
                    // Create new Database Status Model
                    _printerStatusModel = new PrinterStatusViewModel(500);
                    // Add Property Changed event handler
                    //_printerStatusModel.PropertyChanged += OnDatabaseStatusChanged;
                    return _printerStatusModel;
                }
                else
                {
                    return _printerStatusModel;
                }
            }
            set
            {
                _printerStatusModel = value;
                RaisePropertyChanged("PrinterStatusModel");
            }
        }

        public void CheckPrinterStatus(string PrinterName)
        {
            PrinterStatusModel.CheckPrinter(PrinterName, 1800);
        }

        public async Task<bool> CheckPrinterStatusAsync(string PrinterName)
        {
            bool result = await PrinterStatusModel.CheckPrinterAsync(PrinterName, 1800);

            return result;
        }
        #endregion


        #region SignaturePadStatus
        private StatusIconMode _signaturePadStatus;
        public StatusIconMode SignaturePadStatus
        {
            get
            {
                return _signaturePadStatus;
            }
            private set
            {
                _signaturePadStatus = value;
                RaisePropertyChanged("SignaturePadStatus");
            }
        }

        private SignaturePadStatusViewModel _signaturePadStatusModel;
        internal SignaturePadStatusViewModel SignaturePadStatusModel
        {
            get
            {
                if (_signaturePadStatusModel is null)
                {
                    // Create new Database Status Model
                    _signaturePadStatusModel = new SignaturePadStatusViewModel(500);
                    // Add Property Changed event handler
                    //_printerStatusModel.PropertyChanged += OnDatabaseStatusChanged;
                    return _signaturePadStatusModel;
                }
                else
                {
                    return _signaturePadStatusModel;
                }
            }
            set
            {
                _signaturePadStatusModel = value;
                RaisePropertyChanged("SignaturePadStatusModel");
            }
        }

        public void CheckSignaturePadStatus()
        {
            SignaturePadStatusModel.CheckSignaturePad(1000);
        }

        public async Task<bool> CheckSignaturePadStatusAsync()
        {
            return await SignaturePadStatusModel.CheckSignaturePadAsync(1000);
        }
        #endregion
    }
}
