using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Models
{
    public class WizardSelectorItem
    {
        public string Name { get; set;}
        public string Value { get; set; }
        public bool IsSelected { get; set; }
        public string Category { get; set; }
    }

    public class WizardSelectorCategory
    {
        public string Category { get; set; }
    }
}
