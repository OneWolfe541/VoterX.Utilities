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
    public partial class UserPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public UserPage()
        {
            InitializeComponent();
        }

        public UserPage(NMElection election, SystemSettingsController settings, SettingStatus status)
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

        }

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            UserName.Text = _settings.User.UserName;
            UserID.Text = _settings.User.UserID.ToString();
        }


    }
}
