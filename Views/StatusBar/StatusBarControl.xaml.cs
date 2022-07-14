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

namespace VoterX.Utilities.Views
{
    /// <summary>
    /// Interaction logic for StatusBarControl.xaml
    /// </summary>
    public partial class StatusBarControl : UserControl
    {
        public StatusBarControl()
        {
            InitializeComponent();
        }

        private void DatabaseStatusControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Link the Database Status's ViewModel to the current Status Bar View Model
            // The link is created in order for the Status Bar to access the Database Control's properties
            DatabaseStatusControl.DataContext = ((StatusBarViewModel)this.DataContext).DatabaseStatusModel;
        }

        private void PrinterStatusControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Link the Printer Status's ViewModel to the current Status Bar View Model
            // The link is created in order for the Status Bar to access the Printer Control's properties
            PrinterStatusControl.DataContext = ((StatusBarViewModel)this.DataContext).PrinterStatusModel;
        }

        private void SignatureStatusControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Link the Signature Pad Status's ViewModel to the current Status Bar View Model
            // The link is created in order for the Status Bar to access the Signature Pad Control's properties
            SignaturePadStatusControl.DataContext = ((StatusBarViewModel)this.DataContext).SignaturePadStatusModel;
        }
    }
}
