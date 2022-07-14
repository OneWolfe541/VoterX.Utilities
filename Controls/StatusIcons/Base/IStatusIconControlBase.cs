using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Controls
{
    public interface IStatusIconControlBase
    {
        StatusIconMode CurrentStatus { get; }

        bool IconVisibility { get; }
        string IconName { get; }
        bool IconSpin { get; }
        string IconColor { get; }
        string IconToolTip { get; }
    }
}
