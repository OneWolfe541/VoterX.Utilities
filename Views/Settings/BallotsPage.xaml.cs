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
using VoterX.Utilities.UserControls;
using VoterX.Core.Elections;

namespace VoterX.Utilities.Views.Settings
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class BallotsPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public BallotsPage()
        {
            InitializeComponent();
        }

        public BallotsPage(NMElection election, SystemSettingsController settings, SettingStatus status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            DisplaySettings();

            pageLoaded = true;
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            // BALLOT SETTINGS //
            _settings.Ballots.BallotFolder = BallotsFolder.Text;
            _settings.Ballots.ProvisionalFolder = ProvisionalFolder.Text;
            _settings.Ballots.ProvisionalPrefix = ProvisionalPrefix.Text;
            _settings.Ballots.SampleFolder = SampleFolder.Text;
            _settings.Ballots.TestBallot = TestFile.Text;

            _settings.Ballots.Duplex = (bool)DuplexCheck.IsChecked;
        }

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // BALLOT SETTINGS //
            BallotsFolder.Text = _settings.Ballots.BallotFolder;
            ProvisionalFolder.Text = _settings.Ballots.ProvisionalFolder;
            ProvisionalPrefix.Text = _settings.Ballots.ProvisionalPrefix;
            SampleFolder.Text = _settings.Ballots.SampleFolder;
            TestFile.Text = _settings.Ballots.TestBallot;

            DuplexCheck.IsChecked = _settings.Ballots.Duplex;
        }

        // Folder Browser Sample
        // https://stackoverflow.com/questions/1922204/open-directory-dialog
        private string OpenFolderBrowser(string currentPath)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = currentPath;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                return dialog.SelectedPath;
            }
        }

        private string OpenFileBrowser(string currentFile)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.FileName = currentFile;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                return dialog.FileName;
            }
        }

        private void FolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();

            BallotsFolder.Text = OpenFolderBrowser(_settings.Ballots.BallotFolder);
        }

        private void ProvisionalFolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();

            ProvisionalFolder.Text = OpenFolderBrowser(_settings.Ballots.ProvisionalFolder);
        }

        private void SampleFolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();

            SampleFolder.Text = OpenFolderBrowser(_settings.Ballots.SampleFolder);
        }

        private void TestFileBrowser_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();

            TestFile.Text = OpenFileBrowser(_settings.Ballots.TestBallot);
        }

        private void DuplexCheck_Click(object sender, RoutedEventArgs e)
        {
            SettingsChange();
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
    }
}
