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
//using VoterX.Core.Models.Database;
using VoterX.Utilities.UserControls;
using VoterX.Core.Elections;

namespace VoterX.Utilities.Views.Settings
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class TabulatorsPage : SettingsBasePage
    {
        private NMElection _election { get; set; }

        private SystemSettingsController _settings { get; set; }

        private SettingStatus _settingsStatus { get; set; }

        public bool pageLoaded = false;

        public TabulatorsPage()
        {
            InitializeComponent();
        }

        public TabulatorsPage(NMElection election, SystemSettingsController settings, SettingStatus status)
        {
            InitializeComponent();

            _election = election;

            _settings = settings;

            _settingsStatus = status;

            LoadTabulatorList();

            pageLoaded = true;
        }

        private void LoadTabulatorList()
        {
            if (_election != null)
            {
                var list = _election.Lists.Tabulators.Where(tab => tab.PollId == (int)_settings.System.SiteID);

                TabulatorList.ItemsSource = list;
            }
        }

        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/df93d685-844f-47a1-bbbc-454e196369a8/raise-an-event-in-a-child-page-hosted-in-a-frame-to-refresh-a-listbox-on-the-page-hosting-the?forum=wpf
        public override void SaveSettings()
        {
            //PopulateElectionDetails(ElectionList);

            // ELECTION SETTINGS //
            //_settings.Election.ElectionID = Int32.Parse(ElectionID.Text);
            //_settings.Election.CountyCode = CountyCode.Text;
            ////_settings.Election.ElectionType = Int32.Parse(ElectionType.Text);
            //_settings.Election.ElectionType = ComboBoxMethods.GetSelectedItemNumber(ElectionType);
            //_settings.Election.ElectionEntity = ElectionEntity.Text;
            //_settings.Election.ElectionTitle = ElectionTitle.Text;
            //_settings.Election.ElectionDate = DateTime.Parse(ElectionDate.Text);
        }        

        // Need to bind this data instead of manualy stuffing it here
        private void DisplaySettings()
        {
            // ELECTION SETTINGS //
            //PopulateElectionDetails(ElectionList);
        }

        // Scroll the list items
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //// Notes on scroll offsets
            //// https://stackoverflow.com/questions/1033841/is-it-possible-to-implement-smooth-scroll-in-a-wpf-listview
            //// https://social.msdn.microsoft.com/Forums/en-US/3594c80a-7ccf-4cfc-9cc0-9731fd080d72/in-what-unit-is-the-scrollviewerverticaloffset?forum=winappswithcsharp

            ////double delta = (e.Delta * .26978); // Roughly half of 1 list item
            //double delta = (e.Delta * .36);
            ////double delta = (e.Delta / 120)*32; // Reduce to +1 or -1 then multiply to get exact units
            ////StatusBar.ApplicationStatusCenter("Scrolling:" + (delta).ToString());

            //ScrollViewer scv = (ScrollViewer)sender;
            //scv.ScrollToVerticalOffset(scv.VerticalOffset - (delta));
            //e.Handled = true;
        }

        // Scroll the list items from anywhere on the Page
        private void Page_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Mouse Wheel Status
            //StatusBar.ApplicationStatusCenter("Mouse Wheel Move: " + e.Delta);
            //ScrollViewer_PreviewMouseWheel(SearchScrollViewer, e);
        }

    }
}
