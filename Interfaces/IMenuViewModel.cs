using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VoterX.Utilities.Controls;

namespace VoterX.Utilities.Interfaces
{
    public interface IMenuViewModel
    {
        ObservableCollection<Control> HomeCustomControls { get; }

        ObservableCollection<Control> CenterCustomControls { get; }

        ObservableCollection<Control> ExitCustomControls { get; }

        bool HomePanelVisibility { get; set; }

        bool CenterPanelVisibility { get; set; }

        bool ExitPanelVisibility { get; set; }
    }
}
