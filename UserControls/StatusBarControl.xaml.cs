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
using VoterX.Core.Elections;
using AutoVote.Logging;
using VoterX.SystemSettings.Models;
using VoterX.Utilities.Methods;
using VoterX.SystemSettings.Enums;
using VoterX.SystemSettings.Extensions;
//using VoterX.Core.Containers;

namespace VoterX.Utilities.UserControls
{
    /// <summary>
    /// Interaction logic for StatusBarControl.xaml
    /// </summary>
    public partial class StatusBarControl : UserControl
    {
        public string StatusBarLeft
        {
            get { return ApplicationStatusLeftText.Text; }
            set { ApplicationStatusLeftText.Text = value; }
        }

        public string StatusBarRight
        {
            get { return ApplicationStatusRightText.Text; }
            set { ApplicationStatusRightText.Text = value; }
        }

        public string StatusBarCenter
        {
            get { return ApplicationStatusCenterText.Text; }
            set { ApplicationStatusCenterText.Text = value; }
        }                

        private System.Windows.Visibility PrinterIconStatusOK
        {
            get { return ApplicationPrinterStatusOKDisplay.Visibility; }
            set { ApplicationPrinterStatusOKDisplay.Visibility = value; }
        }

        private System.Windows.Visibility PrinterIconStatusBad
        {
            get { return ApplicationPrinterStatusBadDisplay.Visibility; }
            set { ApplicationPrinterStatusBadDisplay.Visibility = value; }
        }

        private System.Windows.Visibility PrinterIconStatusSpinner
        {
            get { return ApplicationPrinterStatusSpinnerDisplay.Visibility; }
            set { ApplicationPrinterStatusSpinnerDisplay.Visibility = value; }
        }

        private System.Windows.Visibility SignatureStatusOK
        {
            get { return ApplicationSignatureStatusOKDisplay.Visibility; }
            set { ApplicationSignatureStatusOKDisplay.Visibility = value; }
        }

        private System.Windows.Visibility SignatureIconStatusBad
        {
            get { return ApplicationSignatureStatusBadDisplay.Visibility; }
            set { ApplicationSignatureStatusBadDisplay.Visibility = value; }
        }

        private System.Windows.Visibility SignatureIconStatusSpinner
        {
            get { return ApplicationSignatureStatusSpinnerDisplay.Visibility; }
            set { ApplicationSignatureStatusSpinnerDisplay.Visibility = value; }
        }

        private System.Windows.Visibility DatabaseIconStatusOK
        {
            get { return ApplicationDatabaseStatusOK.Visibility; }
            set { ApplicationDatabaseStatusOK.Visibility = value; }
        }

        private System.Windows.Visibility DatabaseIconStatusBad
        {
            get { return ApplicationDatabaseStatusBad.Visibility; }
            set { ApplicationDatabaseStatusBad.Visibility = value; }
        }

        private System.Windows.Visibility DatabaseIconStatusSpinner
        {
            get { return ApplicationDatabaseStatusSpinner.Visibility; }
            set { ApplicationDatabaseStatusSpinner.Visibility = value; }
        }

        public async Task<bool> CheckPrinter(PrinterSettingsModel Printers)
        {
            ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed; 
            ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationPrinterStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Visible;

            bool result = await Task.Run(() => PrinterStatus.PrinterIsReadyAsync(Printers.BallotPrinter));
            if (result == true)
            {
                ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationPrinterStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationPrinterStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Collapsed;
            }
            return await Task.Run(()=> true );
        }

        public void HidePrinterStatus()
        {
            ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationPrinterStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void SetPrinterStatus(bool status)
        {
            if (status == true)
            {
                ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ApplicationPrinterStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationPrinterStatusBadDisplay.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public bool GetPrinterStatus()
        {
            if (ApplicationPrinterStatusOKDisplay.Visibility == System.Windows.Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void CheckSignaturePad()
        {
            ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationSignatureStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Visible;

            AutoVoteLogger reportLogger = new AutoVoteLogger("VCCLogs", true);
            if (await Task.Run(() => SignatureMethods.CheckSignaturePadAsync()) != null)
            {
                ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationSignatureStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Collapsed;

                reportLogger.WriteLog("Signature Pad Ready"); 
            }
            else
            {
                ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationSignatureStatusSpinnerDisplay.Visibility = System.Windows.Visibility.Collapsed;
                
                reportLogger.WriteLog("Signature Pad Not Found");
            }
        }

        public void HideSignaturePadStatus()
        {
            ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void SetSignaturePadStatus(bool status)
        {
            if (status == true)
            {
                ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Visible;
                ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ApplicationSignatureStatusOKDisplay.Visibility = System.Windows.Visibility.Collapsed;
                ApplicationSignatureStatusBadDisplay.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public bool GetSignaturePadStatus()
        {
            if (ApplicationSignatureStatusOKDisplay.Visibility == System.Windows.Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HideDatabaseStatus()
        {
            ApplicationDatabaseStatusOK.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationDatabaseStatusBad.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Collapsed;
        }

        public async Task<bool> CheckServer(ElectionFactory election)
        {
            HideDatabaseStatus();

            // Start spinner
            ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Visible;

            AutoVoteLogger reportLogger = new AutoVoteLogger("VCCLogs", true);

            var message = await ServerMethods.CheckConnection(election);
            if (message != "True")
            {
                // stop spinner
                ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Collapsed;
                // display bad icon
                ApplicationDatabaseStatusBad.Visibility = System.Windows.Visibility.Visible;

                ApplicationStatusCenterText.Text = "Database Not Found";
                Console.WriteLine("Database Not Found: " + message);
                reportLogger.WriteLog("Database Not Found: " + message);

                return false;
            }
            else
            {
                // stop spinner
                ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Collapsed;
                // display good icon
                ApplicationDatabaseStatusOK.Visibility = System.Windows.Visibility.Visible;

                reportLogger.WriteLog("Database Connection OK: " + message);

                return true;
            }
        }

        public async Task<bool> CheckServer(NMElection election)
        {
            HideDatabaseStatus();

            // Start spinner
            ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Visible;

            var message = await ServerMethods.CheckConnection(election);
            if (message != "True")
            {
                AutoVoteLogger reportLogger = new AutoVoteLogger("Database", true);

                // stop spinner
                ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Collapsed;
                // display bad icon
                ApplicationDatabaseStatusBad.Visibility = System.Windows.Visibility.Visible;

                ApplicationStatusCenterText.Text = "Database Not Found";
                Console.WriteLine("Database Not Found: " + message);
                reportLogger.WriteLog("Database Not Found: " + message);

                return false;
            }
            else
            {
                // stop spinner
                ApplicationDatabaseStatusSpinner.Visibility = System.Windows.Visibility.Collapsed;
                // display good icon
                ApplicationDatabaseStatusOK.Visibility = System.Windows.Visibility.Visible;

                return true;
            }
        }

        public void ShowLeftSpinner()
        {
            LoadingSpinner.Visibility = Visibility.Visible;
        }

        public void HideLeftSpinner()
        {
            LoadingSpinner.Visibility = Visibility.Collapsed;
        }

        public StatusBarControl()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            ApplicationStatusLeftText.Text = "";
            ApplicationStatusRightText.Text = "";
            ApplicationStatusCenterText.Text = "";
        }

        public void DisplaySystemMode(SystemSettingsModel system)
        {
            switch (system.VCCType)
            {
                case VotingCenterMode.EarlyVoting:
                    ApplicationStatusRightMode.Text = "EV";
                    ApplicationStatusRightMode.ToolTip = system.VCCType.GetStringValue();
                    break;
                case VotingCenterMode.ElectionDay:
                    ApplicationStatusRightMode.Text = "ED";
                    ApplicationStatusRightMode.ToolTip = system.VCCType.GetStringValue();
                    break;
                case VotingCenterMode.SampleBallots:
                    ApplicationStatusRightMode.Text = "SB";
                    ApplicationStatusRightMode.ToolTip = system.VCCType.GetStringValue();
                    break;
            }
        }
    }
}
