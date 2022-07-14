using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.SystemSettings.Models;
using VoterX.Utilities.Commands;

namespace VoterX.Utilities.Controls
{
    public class PrintVerificationQuestionnaireViewModel : NotifyPropertyChanged
    {
        public PrintVerificationQuestionnaireViewModel() : this(null, false) { }
        public PrintVerificationQuestionnaireViewModel(ITroubleShootingQuestionnaireText questionnaireText, bool border)
        {
            if (questionnaireText != null)
            {
                PopulateQuestionnaireText(questionnaireText);
            }

            // Initialize questionnaire state
            ReportQuestionVisibility = true;
            PrinterQuestionVisibility = false;
            ExitQuestionVisibility = false;

            TroubleshootQuestionEnabled = true;

            DividerVisibility = border;
        }

        public string ReportMessage { get; set; }
        public string ReportReprintMessage { get; set; }
        public string ReportQuestion { get; set; }

        public string PrinterMessage { get; set; }
        public string PrinterQuestion { get; set; }

        public string OptionsMessage { get; set; }
        public string ReprintChoiceMessage { get; set; }
        public string ExitChoiceMessage { get; set; }

        public bool? Response { get; set; }
        public bool Reprint { get; private set; }

        public bool DividerVisibility { get; set; }
        public bool ReportQuestionVisibility { get; private set; }
        public bool PrinterQuestionVisibility { get; private set; }
        public bool ExitQuestionVisibility { get; private set; }

        public bool TroubleshootQuestionEnabled { get; set; }

        private void PopulateQuestionnaireText(ITroubleShootingQuestionnaireText questions)
        {
            ReportMessage = questions.ReportMessage;
            ReportReprintMessage = questions.ReprintMessage;
            ReportQuestion = questions.ReportQuestion;
            PrinterMessage = questions.PrinterMessage;
            PrinterQuestion = questions.PrinterQuestion;
            OptionsMessage = questions.OptionsMessage;
            ReprintChoiceMessage = questions.ReprintChoiceMessage;
            ExitChoiceMessage = questions.ExitChoiceMessage;
        }

        #region ReportPrintedQuestionControl
        private YesNoQuestionViewModel _reportPrintedQuestion;
        public YesNoQuestionViewModel ReportPrintedQuestion
        {
            get
            {
                if (_reportPrintedQuestion == null) // Create new question object
                {
                    _reportPrintedQuestion = new YesNoQuestionViewModel(ReportQuestion);
                    // The (Yes/No) question can only be clicked once
                    _reportPrintedQuestion.DisableOnClick = true;
                    // Bind the onchanged method
                    _reportPrintedQuestion.PropertyChanged += OnReportPrintedPropertyChanged;
                }
                return _reportPrintedQuestion;
            }
            private set { _reportPrintedQuestion = value; }
        }

        private void OnReportPrintedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                //Code to respond to a change in the ViewModel
                Console.WriteLine(
                    "Question Clicked: " +
                    ((YesNoQuestionViewModel)sender).IsChecked.ToString());

                // if response is Yes then return true
                if ((bool)((YesNoQuestionViewModel)sender).IsChecked == true)
                {
                    Response = true;
                    RaisePropertyChanged("Response");
                }

                // if the response is No then display the next message
                if ((bool)((YesNoQuestionViewModel)sender).IsChecked == false)
                {
                    // If the control has been reset then exit the questionnaire
                    if (_resetQuestionnaire == true)
                    {
                        ExitChoice = true;
                        RaisePropertyChanged("ExitChoice");
                    }
                    else
                    // Display the next message
                    {
                        if (TroubleshootQuestionEnabled == true)
                        {
                            PrinterQuestionVisibility = true;
                            RaisePropertyChanged("PrinterQuestionVisibility");
                        }
                        else
                        {
                            ExitQuestionVisibility = true;
                            RaisePropertyChanged("ExitQuestionVisibility");
                        }
                    }
                }
            }
        }
        #endregion

        #region PrinterCheckQuestionControl
        private YesNoQuestionViewModel _printerCheckQuestion;
        public YesNoQuestionViewModel PrinterCheckQuestion
        {
            get
            {
                if (_printerCheckQuestion == null)
                {
                    _printerCheckQuestion = new YesNoQuestionViewModel(PrinterQuestion);
                    // The (Yes/No) question can only be clicked once
                    _printerCheckQuestion.DisableOnClick = true;
                    // Bind the onchanged method
                    _printerCheckQuestion.PropertyChanged += OnPrinterCheckPropertyChanged;
                }
                return _printerCheckQuestion;
            }
            private set { _printerCheckQuestion = value; }
        }

        private void OnPrinterCheckPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                //Code to respond to a change in the ViewModel
                Console.WriteLine(
                    "Question Clicked: " +
                    ((YesNoQuestionViewModel)sender).IsChecked.ToString());

                // if response is Yes then return true
                // and set previous question to Yes
                // and hide Printer Check question
                if ((bool)((YesNoQuestionViewModel)sender).IsChecked == true)
                {
                    Response = true;
                    RaisePropertyChanged("Response");

                    _reportPrintedQuestion.YesClicked();

                    // Hide Printer Check questiuon
                    PrinterQuestionVisibility = false;
                    RaisePropertyChanged("PrinterQuestionVisibility");
                }

                // if the response is No then display the exit question
                if ((bool)((YesNoQuestionViewModel)sender).IsChecked == false)
                {
                    ExitQuestionVisibility = true;
                    RaisePropertyChanged("ExitQuestionVisibility");
                }
            }
        }
        #endregion

        #region ExitOptionCommands
        // Allow the user to toggle between the reprint and exit options
        private bool _reprintChoice;
        public bool ReprintChoice
        {
            get { return _reprintChoice; }
            set
            {
                if (_reprintChoice == value)
                    return;
                else
                {
                    _reprintChoice = value;
                    if (_reprintChoice == true)
                    {
                        Console.WriteLine("Reprint is Selected");

                        Reprint = true;
                        RaisePropertyChanged("Reprint");

                        ExitChoice = false;
                        RaisePropertyChanged("ExitChoice");
                    }
                    else
                    {
                        Reprint = false;
                        RaisePropertyChanged("Reprint");
                    }
                }
            }
        }

        private bool _exitChoice;
        public bool ExitChoice
        {
            get { return _exitChoice; }
            set
            {
                if (_exitChoice == value) return;
                else
                {
                    _exitChoice = value;
                    if (_exitChoice == true)
                    {
                        Console.WriteLine("Exit is Selected");

                        Response = false;
                        RaisePropertyChanged("Response");

                        ReprintChoice = false;
                        RaisePropertyChanged("ReprintChoice");
                    }
                    else
                    {
                        // response cannot be true

                        Response = null;
                        RaisePropertyChanged("Response");
                    }
                }
            }
        }

        // User cannot toggle between the reprint and exit options
        //public RelayCommand _reprintChoiceCommand;
        //public ICommand ReprintChoiceCommand
        //{
        //    get
        //    {
        //        if (_reprintChoiceCommand == null)
        //        {
        //            _reprintChoiceCommand = new RelayCommand(param => this.ReprintChoiceClick(param), param => this.CanClickReprint);
        //        }
        //        return _reprintChoiceCommand;
        //    }
        //}

        //public void ReprintChoiceClick(object test)
        //{
        //    Console.WriteLine(test.ToString());

        //    ExitChoice2 = false;
        //    RaisePropertyChanged("ExitChoice2");
        //}

        //private bool _canClickReprint;
        //public bool CanClickReprint
        //{
        //    get { return _canClickReprint; }
        //    internal set
        //    {
        //        _canClickReprint = value;
        //        RaisePropertyChanged("CanClickReprint");
        //    }
        //}        

        //public RelayCommand _exitChoiceCommand;
        //public ICommand ExitChoiceCommand
        //{
        //    get
        //    {
        //        if (_exitChoiceCommand == null)
        //        {
        //            _exitChoiceCommand = new RelayCommand(param => this.ExitChoiceClick(param), param => this.CanClickExit);
        //        }
        //        return _exitChoiceCommand;
        //    }
        //}

        //public void ExitChoiceClick(object test)
        //{
        //    Console.WriteLine(test.ToString());

        //    ReprintChoice1 = false;
        //    RaisePropertyChanged("ReprintChoice1");
        //}

        //private bool _canClickExit;
        //public bool CanClickExit
        //{
        //    get { return _canClickExit; }
        //    internal set
        //    {
        //        _canClickExit = value;
        //        RaisePropertyChanged("CanClickExit");
        //    }
        //}

        private bool _resetQuestionnaire = false;
        public void Reset()
        {
            // Set reset flag
            _resetQuestionnaire = true;

            // Clear the response
            Response = null;

            // Display new message
            ReportMessage = ReportReprintMessage;
            RaisePropertyChanged("ReportMessage");

            // Hide the printer check question
            PrinterQuestionVisibility = false;
            RaisePropertyChanged("PrinterQuestionVisibility");

            // Hide the exit question
            ExitQuestionVisibility = false;
            RaisePropertyChanged("ExitQuestionVisibility");

            // Reset the printer check question
            var newPrintedQuestion = new YesNoQuestionViewModel(ReportQuestion);
            // The (Yes/No) question can only be clicked once
            newPrintedQuestion.DisableOnClick = true;
            // Bind the onchanged method
            newPrintedQuestion.PropertyChanged += OnReportPrintedPropertyChanged;

            ReportPrintedQuestion = newPrintedQuestion;
            RaisePropertyChanged("ReportPrintedQuestion");

            // Reset the exit question
            var newPrinterCheckQuestion = new YesNoQuestionViewModel(PrinterQuestion);
            // The (Yes/No) question can only be clicked once
            newPrinterCheckQuestion.DisableOnClick = true;
            // Bind the onchanged method
            newPrinterCheckQuestion.PropertyChanged += OnPrinterCheckPropertyChanged;

            PrinterCheckQuestion = newPrinterCheckQuestion;
            RaisePropertyChanged("PrinterCheckQuestion");

            // Clear the reprint checkbox
            ReprintChoice = false;
            RaisePropertyChanged("ReprintChoice");

            // Clear the exit checkbox
            ExitChoice = false;
            RaisePropertyChanged("ExitChoice");
        }
        #endregion
    }
}
