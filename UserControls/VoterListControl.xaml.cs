using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoterX.Core.Voters;

namespace VoterX.Utilities.UserControls
{
    /// <summary>
    /// Interaction logic for VoterListControl.xaml
    /// </summary>
    public partial class VoterListControl : UserControl
    {
        public event RoutedEventHandler VoterClick;

        public NMVoter SelectedVoter { get; private set; }        

        public VoterListControl()
        {
            InitializeComponent();
        }

        public void SetVoterList(ObservableCollection<NMVoter> list)
        {
            SearchScrollViewer.ScrollToTop();

            VoterList.ItemsSource = list;
        }

        public void ClearList()
        {
            SearchingPanel.Visibility = Visibility.Collapsed;
            SearchResults.Visibility = Visibility.Collapsed;
            SearchResults.Text = "";

            SearchScrollViewer.ScrollToTop();

            VoterList.ItemsSource = null;
        }

        public void StartSearchAnimation()
        {
            SearchingPanel.Visibility = Visibility.Visible;
        }

        public void EndSearchAnimation()
        {            
            SearchingPanel.Visibility = Visibility.Collapsed;
        }

        public void StartLoadAnimation()
        {
            LoadingPanel.Visibility = Visibility.Visible;
        }

        public void EndLoadAnimation()
        {
            LoadingPanel.Visibility = Visibility.Collapsed;
        }

        public void DisplayResults(string message)
        {
            SearchResults.Visibility = Visibility.Visible;
            SearchResults.Text = message;
        }

        public void ScrollList(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer_PreviewMouseWheel(SearchScrollViewer, e);
        }

        // Scroll the list items
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Notes on scroll offsets
            // https://stackoverflow.com/questions/1033841/is-it-possible-to-implement-smooth-scroll-in-a-wpf-listview
            // https://social.msdn.microsoft.com/Forums/en-US/3594c80a-7ccf-4cfc-9cc0-9731fd080d72/in-what-unit-is-the-scrollviewerverticaloffset?forum=winappswithcsharp

            //double delta = (e.Delta * .26978); // Roughly half of 1 list item
            double delta = (e.Delta * .36);
            //double delta = (e.Delta / 120)*32; // Reduce to +1 or -1 then multiply to get exact units
            //StatusBar.ApplicationStatusCenter("Scrolling:" + (delta).ToString());

            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - (delta));
            e.Handled = true;
        }

        // Send selected voter and the search parameters to the next page
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                SelectedVoter = ((NMVoter)item.DataContext);
                VoterClick?.Invoke(sender, e);
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                ((Button)sender).IsEnabled = false;
            }
        }

        private void VoterList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
            }
        }
    }
}
