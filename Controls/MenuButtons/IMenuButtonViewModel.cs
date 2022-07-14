using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VoterX.Utilities.Controls
{
    public interface IMenuButtonViewModel
    {
        FontAwesome.WPF.FontAwesomeIcon Icon { get; set; }
        string Text { get; set; }
        string ToolTip { get; set; }
        Thickness Margin { get; set; }
        ICommand NavigateCommand { get; }
    }
}
