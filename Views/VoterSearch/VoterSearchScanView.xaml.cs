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
    /// Interaction logic for VoterSearchScanView.xaml
    /// </summary>
    public partial class VoterSearchScanView : UserControl
    {
        public VoterSearchScanView()
        {
            InitializeComponent();

            Keyboard.Focus(BarCodeSearch);
        }

        private void BarCodeSearch_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(BarCodeSearch);
        }

        public void SetInputFocus()
        {
            Keyboard.Focus(BarCodeSearch);
        }
    }
}
