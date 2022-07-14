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
using VoterX.Utilities.Methods;
using VoterX.Utilities.BasePageDefinitions;
//using VoterX.Core.Containers;
//using VoterX.Core.Repositories;
using VoterX.SystemSettings;
using VoterX.Utilities.Models;
using VoterX.SystemSettings.Models;
using VoterX.SystemSettings.Methods;
using VoterX.Utilities.UserControls;
using AutoVote.Logging;
using VoterX.Core.Elections;

namespace VoterX.Utilities.Views.Settings
{
    public class PrinterPageReady
    {
        public bool BallotPrinterReady;
        public bool BallotSizeReady;
        public bool BallotBinReady;

        public bool ReportPrinterReady;
        public bool ReportSizeReady;
        public bool ReportBinReady;

        public bool SamplePrinterReady;
        public bool SampleSizeReady;
        public bool SampleBinReady;

        public bool IsReady()
        {
            if (
                BallotPrinterReady == true &&
                BallotSizeReady == true &&
                BallotBinReady == true &&
                ReportPrinterReady == true &&
                ReportSizeReady == true &&
                ReportBinReady == true &&
                SamplePrinterReady == true &&
                SampleSizeReady == true &&
                SampleBinReady == true
                ) return true;
            else
                return false;
        }
    }

    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class PrintersPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private IEnumerable<string> printerList = PrinterStatus.PrintersList();

        //private List<PrinterLookupModel> LargePrinterList = new List<PrinterLookupModel>();

        PrinterPageReady _pageReady = new PrinterPageReady();

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        Frame _parent = null;

        AutoVoteLogger settingsLog;

        public PrintersPage()
        {
            InitializeComponent();

            settingsLog = new AutoVoteLogger("VCClogs", true);
            settingsLog.WriteLog("Loading Printer Settings Page");

            pageLoaded = true;
        }

        public PrintersPage(Frame parent)
        {
            InitializeComponent();

            _parent = parent;

            settingsLog = new AutoVoteLogger("VCClogs", true);
            settingsLog.WriteLog("Loading Printer Settings Page");

            pageLoaded = true;
        }

        public PrintersPage(NMElection election, SystemSettingsController settings)
        {
            InitializeComponent();

            settingsLog = new AutoVoteLogger("VCClogs", true);
            settingsLog.WriteLog("Loading Printer Settings Page");

            _election = election;

            _settings = settings;

            LoadPrinters();

            DisplaySettings();

            pageLoaded = true;
        }

        public PrintersPage(NMElection election, SystemSettingsController settings, Frame parent, SettingStatus status)
        {
            InitializeComponent();

            settingsLog = new AutoVoteLogger("VCClogs", true);
            settingsLog.WriteLog("Loading Printer Settings Page");

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            _parent = parent;

            LoadPrinters();

            DisplaySettings();

            pageLoaded = true;
        }

        public PrintersPage(NMElection election, SystemSettingsController settings, Frame parent, bool reload, SettingStatus status)
        {
            InitializeComponent();

            settingsLog = new AutoVoteLogger("VCClogs", true);
            settingsLog.WriteLog("Loading Printer Settings Page");

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            _parent = parent;

            ReloadPrinterLists();

            LoadPrinters();

            DisplaySettings();

            pageLoaded = true;
        }

        private void LoadPrinters()
        {
            try
            {
                //if (printerList != null)
                //{
                //    settingsLog.WriteLog("Load Printers " + printerList.ToString());
                //}

                LoadPrinterListIntoComboBox(BallotPrinter, _settings.Printers.BallotPrinter.ToString());
                _pageReady.BallotPrinterReady = true;
                LoadPrinterListIntoComboBox(AppPrinter, _settings.Printers.ApplicationPrinter.ToString());
                _pageReady.ReportPrinterReady = true;
                LoadPrinterListIntoComboBox(SamplePrinter, _settings.Printers.SamplePrinter.ToString());
                _pageReady.SamplePrinterReady = true;
                LoadPrinterListIntoComboBox(ReportPrinter, _settings.Printers.ReportPrinter.ToString());
                //_pageReady.ReportPrinterReady = true;
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Printers] " + e.Message);
            }
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            //if (_pageReady.IsReady() == true)
            {
                // PRINTER SETTINGS //
                //_settings.Printers.BallotPrinter = GetSelectedItem(BallotPrinter);
                //_settings.Printers.BallotSize = GetSelectedItemNumber(BallotSize);
                //_settings.Printers.BallotBin = GetSelectedItemNumber(BallotBin);

                //_settings.Printers.ApplicationPrinter = GetSelectedItem(AppPrinter);
                //_settings.Printers.AppSize = GetSelectedItemNumber(AppSize);
                //_settings.Printers.AppBin = GetSelectedItemNumber(AppBin);

                //_settings.Printers.SamplePrinter = GetSelectedItem(SamplePrinter);
                //_settings.Printers.SampleSize = GetSelectedItemNumber(SampleSize);
                //_settings.Printers.SampleBin = GetSelectedItemNumber(SampleBin);

                //_settings.Printers.ReportPrinter = GetSelectedItem(ReportPrinter);
                //_settings.Printers.ReportSize = GetSelectedItemNumber(ReportSize);
                //_settings.Printers.ReportBin = GetSelectedItemNumber(ReportBin);

                _settings.Printers.BallotPrinter = GetSelectedItem(BallotPrinter);
                _settings.Printers.BallotSize = GetIntFromString(BallotSizeBox.Text);
                _settings.Printers.BallotBin = GetIntFromString(BallotBinBox.Text);

                _settings.Printers.ApplicationPrinter = GetSelectedItem(AppPrinter);
                _settings.Printers.AppSize = GetIntFromString(AppSizeBox.Text);
                _settings.Printers.AppBin = GetIntFromString(AppBinBox.Text);

                _settings.Printers.SamplePrinter = GetSelectedItem(SamplePrinter);
                _settings.Printers.SampleSize = GetIntFromString(SampleSizeBox.Text);
                _settings.Printers.SampleBin = GetIntFromString(SampleBinBox.Text);

                //_settings.Printers.LowQualityBallot = (bool)DraftQualityCheck.IsChecked;
            }
        }        

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // PRINTER SETTINGS //
            if (_settings.Printers.NoPad) SignaturePad.Foreground = new SolidColorBrush(Colors.Green);
            else SignaturePad.Foreground = new SolidColorBrush(Colors.Red);
            //SignaturePad.Text = _settings.Printers.NoPad.ToString();
            //BallotSize.Text = _settings.Printers.BallotSize.ToString();
            //AppSize.Text = _settings.Printers.AppSize.ToString();
            //SampleSize.Text = _settings.Printers.SampleSize.ToString();
            //ReportSize.Text = _settings.Printers.ReportSize.ToString();

            BallotSizeBox.Text = _settings.Printers.BallotSize.ToString();
            BallotBinBox.Text = _settings.Printers.BallotBin.ToString();
            AppSizeBox.Text = _settings.Printers.AppSize.ToString();
            AppBinBox.Text = _settings.Printers.AppBin.ToString();
            SampleSizeBox.Text = _settings.Printers.SampleSize.ToString();
            SampleBinBox.Text = _settings.Printers.SampleBin.ToString();
            //DraftQualityCheck.IsChecked = _settings.Printers.LowQualityBallot;
        }

        private void LoadPrinterListIntoComboBox(object sender, string selectedItem)
        {
            // Add blank item to list
            AddComboItemToControl(sender, "", "");

            try
            {
                // Loop through list of printers 
                foreach (string printerName in printerList)
                {
                    // Add each printer name to the combo box control
                    AddComboItemToControl(sender, printerName, selectedItem);
                }
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Printers into Control] Printer:" + selectedItem + " " + e.Message);
            }
        }

        //private void StartComboBoxLoadingMessage(ComboBox sender)
        //{
        //    var loadingItem = ComboBoxMethods.AddLoadingItem((ComboBox)sender, TempLoadingSpinnerItem);
        //}

        // Add a single item to a combo box
        private void AddComboItemToControl(object sender, string newItem, string selectedItem)
        {
            // Create blank list item
            ComboBoxItem item = new ComboBoxItem();
            // Set list item name
            item.Content = newItem.ToUpper();
            // Check default for null and empty strings
            if (selectedItem != null && selectedItem != "" && selectedItem.Replace(" ", "") != "")
            {
                // Set selected item default from given string
                if (newItem.ToUpper().Contains(selectedItem.ToUpper()))
                    item.IsSelected = true;
            }
            // Add the item to the combo box
            ((ComboBox)sender).Items.Add(item);
        }

        // Add a single item to a combo box
        private void AddComboItemToControl(object sender, int itemIndex, string itemName, int selectedIndex)
        {
            // Create blank list item
            ComboBoxItem item = new ComboBoxItem();

            // Set list item name
            item.Content = itemName.ToUpper();

            // Set list item index
            item.DataContext = itemIndex;

            // Set default selected item
            if (itemIndex == selectedIndex) item.IsSelected = true;

            // Add the item to the combo box
            ((ComboBox)sender).Items.Add(item);
        }

        private async Task<bool> LoadBinList(object sender, string printerName, int selectedItem)
        {
            var loadingItem = ComboBoxMethods.AddLoadingItem((ComboBox)sender, TempLoadingSpinnerItem);

            // Add blank item to list
            AddComboItemToControl(sender, "", "");

            List<SystemSettings.Models.PrinterTray> trayList = null;

            try
            {
                trayList = await _settings.Lists.GetPrinterTraysAsync(printerName);
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Trays from System] " + e.Message);
            }

            try
            {
                // Loop through the list of paper trays
                //foreach (Utilities.Models.PrinterTray tray in await PrinterStatus.TraysList(printerName))
                foreach (var tray in trayList)
                {
                    // Add each item to the combo box control
                    AddComboItemToControl(sender, tray.Index, tray.Name, selectedItem);
                }
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Trays into Control] " + e.Message);
            }

            ComboBoxMethods.RemoveListItem(sender, loadingItem);

            return true;
        }

        private async Task<bool> LoadPaperSizeList(object sender, string printerName, int selectedItem)
        {
            var loadingItem = ComboBoxMethods.AddLoadingItem((ComboBox)sender, TempLoadingSpinnerItem);

            // Add blank item to list
            AddComboItemToControl(sender, "", "");

            List<SystemSettings.Models.PaperSize> sizeList = null;

            try
            {
                sizeList = await _settings.Lists.GetPaperSizesAsync(printerName);
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Paper Sizes from Printer] " + e.Message);
            }

            try
            {
                // Loop through the list of paper sizes
                //foreach (Utilities.Models.PaperSize paper in await PrinterStatus.PaperSizesList(printerName))
                foreach (var paper in sizeList)
                {
                    // Add each item to the combo box control
                    AddComboItemToControl(sender, paper.Index, paper.Name, selectedItem);
                }
            }
            catch (Exception e)
            {
                settingsLog.WriteLog("Printer Settings Error: [Loading Paper Sizes into Control] " + e.Message);
            }

            ComboBoxMethods.RemoveListItem(sender, loadingItem);

            return true;
        }

        private string GetSelectedItem(ComboBox sender)
        {
            if (sender.SelectedItem == null) return "";
            else
            {
                if (((ComboBoxItem)sender.SelectedItem).Content == null) return "";
                else
                    return ((ComboBoxItem)sender.SelectedItem).Content.ToString();
            }
        }

        private int GetSelectedItemNumber(ComboBox sender)
        {
            if (sender.SelectedItem == null) return 0;
            else
            {
                if (((ComboBoxItem)sender.SelectedItem).DataContext == null) return 0;
                else
                    return (int)((ComboBoxItem)sender.SelectedItem).DataContext;
            }
        }

        // Change the Ballot Bins list when the Ballot Printer changes
        // Also runs when the drop down box loads for the first time
        private async void BallotPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if combo box is loaded 
            // https://stackoverflow.com/questions/5022201/combobox-selectionchanged-event-triggers-without-even-changing-the-selection-in
            //var comboBox = (ComboBox)sender;
            //if (!comboBox.IsLoaded)
            //{
            //    // This is when the combo box is not loaded yet and the event is called.
            //    return;
            //}

            // Clear the combo box
            BallotBin.Items.Clear();

            _pageReady.BallotBinReady = false; 
            _pageReady.BallotSizeReady = false;

            if (BallotPrinter.SelectedIndex > 0)
            {
                var binboxtest = BallotBin;
                var printertest = GetSelectedItem(BallotPrinter);
                var bintest = _settings.Printers.BallotBin;

                await LoadBinList(
                // Where to load the list into
                BallotBin,
                // Which printer to get list from
                GetSelectedItem(BallotPrinter),
                // Default Value from Global object
                _settings.Printers.BallotBin
                );
            }

            _pageReady.BallotBinReady = true;

            // Clear the combo box
            BallotSize.Items.Clear();

            if (BallotPrinter.SelectedIndex > 0)
            {
                await LoadPaperSizeList(
                // Where to load the list into
                BallotSize,
                // Which printer to get list from
                GetSelectedItem(BallotPrinter),
                // Default Value from Global object
                _settings.Printers.BallotSize
                );
            }

            _pageReady.BallotSizeReady = true;
        }

        // Load the list into the Ballot Printer combo box
        private void BallotPrinter_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPrinterListIntoComboBox(sender, _settings.Printers.BallotPrinter.ToString());
        }

        // Change the Application Bins list when the Application Printer changes
        // Also runs when the drop down box loads for the first time
        private async void AppPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comboBox = (ComboBox)sender;
            //if (!comboBox.IsLoaded)
            //{
            //    // This is when the combo box is not loaded yet and the event is called.
            //    return;
            //}

            // Clear the combo box
            AppBin.Items.Clear();

            _pageReady.ReportBinReady = false;
            _pageReady.ReportSizeReady = false;

            if (AppPrinter.SelectedIndex > 0)
            {
                await LoadBinList(
                // Where to load the list into
                AppBin,
                // Which printer to get list from                    
                GetSelectedItem(AppPrinter),
                // Default Value from Global object
                _settings.Printers.AppBin
                );
            }

            _pageReady.ReportBinReady = true;

            // Clear the combo box
            AppSize.Items.Clear();

            if (AppPrinter.SelectedIndex > 0)
            {
                await LoadPaperSizeList(
                // Where to load the list into
                AppSize,
                // Which printer to get list from
                GetSelectedItem(AppPrinter),
                // Default Value from Global object
                _settings.Printers.AppSize
                );
            }

            _pageReady.ReportSizeReady = true;
        }

        // Load the list into the Application Printer combo box
        private void AppPrinter_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPrinterListIntoComboBox(sender, _settings.Printers.ApplicationPrinter.ToString());
        }

        // Change the Sample Bins list when the Sample Printer changes
        // Also runs when the drop down box loads for the first time
        private async void SamplePrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comboBox = (ComboBox)sender;
            //if (!comboBox.IsLoaded)
            //{
            //    // This is when the combo box is not loaded yet and the event is called.
            //    return;
            //}

            // Clear the combo box
            SampleBin.Items.Clear();

            _pageReady.SampleBinReady = false;
            _pageReady.SampleSizeReady = false;

            if (SamplePrinter.SelectedIndex > 0)
            {
                await LoadBinList(
                // Where to load the list into
                SampleBin,
                // Which printer to get list from                    
                GetSelectedItem(SamplePrinter),
                // Default Value from Global object
                _settings.Printers.SampleBin
                );
            }

            _pageReady.SampleBinReady = true;

            // Clear the combo box
            SampleSize.Items.Clear();

            if (SamplePrinter.SelectedIndex > 0)
            {
                await LoadPaperSizeList(
                // Where to load the list into
                SampleSize,
                // Which printer to get list from
                GetSelectedItem(SamplePrinter),
                // Default Value from Global object
                _settings.Printers.SampleSize
                );
            }

            _pageReady.SampleSizeReady = true;
        }

        // Load the list into the Sample Printer combo box
        private void SamplePrinter_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPrinterListIntoComboBox(sender, _settings.Printers.SamplePrinter.ToString());
        }

        // Change the Report Bins list when the Report Printer changes
        // Also runs when the drop down box loads for the first time
        private async void ReportPrinter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comboBox = (ComboBox)sender;
            //if (!comboBox.IsLoaded)
            //{
            //    // This is when the combo box is not loaded yet and the event is called.
            //    return;
            //}

            // Clear the combo box
            ReportBin.Items.Clear();

            _pageReady.ReportBinReady = false;
            _pageReady.ReportSizeReady = false;

            if (ReportPrinter.SelectedIndex > 0)
            {
                await LoadBinList(
                    // Where to load the list into
                    ReportBin,
                    // Which printer to get list from                    
                    GetSelectedItem(ReportPrinter),
                    // Default Value from Global object
                    _settings.Printers.ReportBin
                    );
            }

            _pageReady.ReportBinReady = true;

            // Clear the combo box
            ReportSize.Items.Clear();

            if (ReportPrinter.SelectedIndex > 0)
            {
                await LoadPaperSizeList(
                    // Where to load the list into
                    ReportSize,
                    // Which printer to get list from
                    GetSelectedItem(ReportPrinter),
                    // Default Value from Global object
                    _settings.Printers.ReportSize
                    );
            }

            _pageReady.ReportSizeReady = true;
        }

        // Load the list into the Report Printer combo box
        private void ReportPrinter_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPrinterListIntoComboBox(sender, _settings.Printers.ReportPrinter.ToString());
        }

        private void LoadPrintersButton_Click(object sender, RoutedEventArgs e)
        {
            //RestartAllLoadingBoxes();
            //var done = await _settings.ReloadPrinterLists();
            //if (done == true)
            //{
            //    LoadPrinters();
            //}

            _parent.Navigate(new Views.Settings.PrintersPage(_election, _settings, _parent, true, _settingsStatus));
        }

        private async void ReloadPrinterLists()
        {
            var done = await _settings.ReloadPrinterLists();
            if (done == true)
            {
                //LoadPrinters();
            }
        }

        private void SavePrintersButton_Click(object sender, RoutedEventArgs e)
        {
            //_settings.CreatePrinterListFile(_settings.Lists.Printers);
        }

        private void BallotSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BallotSizeBox.Text = GetSelectedItemNumber(BallotSize).ToString();
        }

        private void BallotBin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BallotBinBox.Text = GetSelectedItemNumber(BallotBin).ToString();
        }

        private void AppSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppSizeBox.Text = GetSelectedItemNumber(AppSize).ToString();
        }

        private void AppBin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppBinBox.Text = GetSelectedItemNumber(AppBin).ToString();
        }

        private void SampleSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SampleSizeBox.Text = GetSelectedItemNumber(SampleSize).ToString();
        }

        private void SampleBin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SampleBinBox.Text = GetSelectedItemNumber(SampleBin).ToString();
        }

        private int GetIntFromString(string input)
        {
            int output = 0;
            if(Int32.TryParse(input, out output) == true)
            {
                return output;
            }
            else
            {
                return 0;
            }
        }

        private void SettingsChange()
        {
            _settingsStatus.SettingsChanged = true;
        }

        private void SettingsChanged_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((ComboBox)sender).IsLoaded)
            {
                SettingsChange();
            }
        }

        private void SettingsChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                SettingsChange();
            }
        }

        private void DraftQualityCheck_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();
        }
    }
}
