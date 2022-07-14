using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbirSigPad;
using VoterX.Core.Voters;

namespace VoterX.Utilities.Methods
{
    public static class AmbirSignatureMethods
    {
        public static AmbirSignaturePad Create()
        {
            return new AmbirSignaturePad();
        }

        public static AmbirSigPad.Voter SignatureMetadata(NMVoter voter, string siteId, string siteName)
        {
            return new AmbirSigPad.Voter()
            {
                VoterId = voter.Data.VoterID,
                FirstName = voter.Data.FirstName,
                LastName = voter.Data.LastName,
                MiddleName = voter.Data.MiddleName,
                Generation = voter.Data.Generation,
                Location = siteId,
                SiteName = siteName,
                ElectionId = voter.Data.ElectionID.ToString(),
                County = voter.Data.County
            };
        }

        public static AmbirSigPad.Voter SignatureMetadata(string fileName, string siteId, string siteName)
        {
            return new AmbirSigPad.Voter()
            {
                VoterId = fileName,
                FirstName = "Site",
                LastName = "Tester",
                MiddleName = " ",
                Generation = " ",
                Location = siteId,
                SiteName = siteName,
                ElectionId = " ",
                County = " "
            };
        }
    }
}
