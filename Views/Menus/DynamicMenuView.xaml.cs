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
    /// Interaction logic for DynamicMenuView.xaml
    /// </summary>
    public partial class DynamicMenuView : Page
    {
        //private object _menuViewModel;

        public DynamicMenuView() : this(null) { }
        public DynamicMenuView(object MenuViewModel)
        {
            InitializeComponent();

            //_menuViewModel = MenuViewModel;

            this.DataContext = MenuViewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = _menuViewModel;
        }
    }
}
