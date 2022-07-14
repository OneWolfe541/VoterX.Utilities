using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Voters;

namespace VoterX.Utilities.Views.VoterDetails
{
    public class VoterDetailsViewModel
    {
        // Dont use Observable Collections for single voter display
        // Have to use INotifyPropertyChanged when not using an Observable Collection
        public NMVoter VoterItem { get; set; }

        public VoterDetailsViewModel(NMVoter voter)
        {
            VoterItem = voter;
        }
    }
}
