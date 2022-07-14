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

namespace VoterX.Utilities.Controls
{
    /// <summary>
    /// Interaction logic for YesNoQuestionControlMVVM.xaml
    /// </summary>
    public partial class YesNoQuestionControlMVVM : UserControl
    {
        public YesNoQuestionControlMVVM()
        {
            InitializeComponent();
        }

        // Does not work
        public double QuestionHeight
        {
            get { return QuestionBorder.Height; }
            set
            {
                //double temp = value;
                QuestionBorder.Height = value;
                ToggleCheckYes.Height = value;
                ToggleCheckNo.Height = value;
            }
        }

        public double QuestionWidth
        {
            get { return QuestionBorder.Width; }
            set { QuestionBorder.Width = value; }
        }

        // Does not work
        public double QuestionFontSize
        {
            get { return TextQuestion.FontSize; }
            set
            {
                double temp = value == 0 ? this.FontSize : value;
                TextQuestion.FontSize = temp;
                ToggleCheckYes.FontSize = temp;
                toggle_check_fa_check_yes.Height = temp;
                toggle_check_fa_check_yes.Width = temp;
                ToggleCheckNo.FontSize = temp;
                toggle_check_fa_check_no.Height = temp;
                toggle_check_fa_check_no.Width = temp;
            }
        }
    }
}
