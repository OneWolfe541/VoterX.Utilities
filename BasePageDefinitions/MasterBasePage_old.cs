using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FontAwesome.WPF;

namespace VoterX.Utilities.BasePageDefinitions
{
    public abstract class MasterBasePage_old : Page
    {
        public abstract string PageHeaderName { get; set; }

        public abstract string ApplicationStatusLeft { get; set; }

        public abstract string ApplicationStatusRight { get; set; }

        public abstract string ApplicationStatusCenter { get; set; }

        public abstract System.Windows.Visibility ApplicationPrinterStatusOK { get; set; }

        public abstract System.Windows.Visibility ApplicationPrinterStatusBad { get; set; }

        public abstract System.Windows.Visibility ApplicationPrinterStatusSpinner { get; set; }

        public abstract System.Windows.Visibility ApplicationSignatureStatusOK { get; set; }

        public abstract System.Windows.Visibility ApplicationSignatureStatusBad { get; set; }

        public abstract System.Windows.Visibility ApplicationSignatureStatusSpinner { get; set; }
    }
}
