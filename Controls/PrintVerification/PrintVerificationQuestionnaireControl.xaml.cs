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
    /// Interaction logic for PrintVerificationQuestionnaireControl.xaml
    /// </summary>
    public partial class PrintVerificationQuestionnaireControl : UserControl
    {
        public PrintVerificationQuestionnaireControl()
        {
            InitializeComponent();
        }

        public double QuestionWidth
        {
            get { return ReportCheckQuestion.QuestionWidth; }
            set
            {
                ReportCheckQuestion.QuestionWidth = value;
                PrinterCheckQuestion.QuestionWidth = value;
                ExitChoice1Border.Width = value + 120;
                ExitChoice2Border.Width = value + 120;
            }
        }
    }
}
