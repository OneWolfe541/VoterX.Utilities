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
    /// Interaction logic for WizardTwoLevelSelectorView.xaml
    /// </summary>
    public partial class WizardTwoLevelSelectorView : UserControl
    {
        public WizardTwoLevelSelectorView()
        {
            InitializeComponent();
        }

        public double ViewPanelHeight
        {
            get
            {
                return ItemList.Height;
            }
            set
            {
                ItemList.Height = value;
            }
        }

        public double ViewPanelWidth
        {
            get
            {
                return ItemList.Width;
            }
            set
            {
                ItemList.Width = value;
                CategoriesList.Width = ItemList.Width;
                ClearListButton.Width = ItemList.Width - 5;
            }
        }
    }
}
