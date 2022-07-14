using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Voters;

namespace VoterX.Utilities.Methods
{
    public enum VoterLookupStatus
    {
        Eligible = 1,
        Ineligible = 2,
        Spoilable = 3,
        Provisional = 4,
        Deleted = 5,
        Hybrid = 6,
        None = 0
    };

    public static class VoterValidationMethods
    {
        // This function checks if the voter has already voted and where
        // for deciding what options are availible to the user 
        public static VoterLookupStatus CheckVoterStatus(NMVoter voter, int pollId)
        {
            VoterLookupStatus voterStatus = VoterLookupStatus.None;

            // Check deleted status
            if (voter.WasRemoved())
            {
                voterStatus = VoterLookupStatus.Deleted;
            }
            // Check provisional Only
            else if (voter.Data.ProvisionalOnly == true)
            {
                voterStatus = VoterLookupStatus.Provisional;
            }
            // Check log code
            else if (voter.HasVoted())
            {

                //Check voter location and logdate
                if (voter.VotedHereToday(pollId) && !voter.WrongOrFledVoter())
                {
                    // if same location then spoil ballot
                    voterStatus = VoterLookupStatus.Spoilable;
                }
                else
                {
                    // if different location or date then provisional ballot
                    voterStatus = VoterLookupStatus.Provisional;
                }
            }
            // Check if voter has a valid ballot style
            else if (!voter.IsEligible())
            {
                //IneligibleVoter
                voterStatus = VoterLookupStatus.Ineligible;
            }

            // Add precinct check for Hybrid sites (set voterStatus = VoterLookupStatus.Provisional when not for current site)
            // Voter gets official ballot if they exist in ElectionPrecinctPoll
            // Other wise they get a provisional

            else
            {
                // Allow user to procceed to print out a ballot
                voterStatus = VoterLookupStatus.Eligible;
            }

            return voterStatus;
        }


        public static VoterLookupStatus CheckVoterStatus(NMVoter voter)
        {
            VoterLookupStatus voterStatus = VoterLookupStatus.None;

            return voterStatus;
        }

        public static VoterLookupStatus CheckVoterStatusHybrid(NMVoter voter, int pollId)
        {
            VoterLookupStatus voterStatus = VoterLookupStatus.None;

            // Check deleted status
            if (voter.WasRemoved())
            {
                voterStatus = VoterLookupStatus.Deleted;
            }
            // Check provisional Only
            else if (voter.Data.ProvisionalOnly == true)
            {
                voterStatus = VoterLookupStatus.Provisional;
            }
            // Check log code
            else if (voter.HasVoted())
            {

                //Check voter location and logdate
                if (voter.VotedHereToday(pollId) && !voter.WrongOrFledVoter())
                {
                    // if same location then spoil ballot
                    voterStatus = VoterLookupStatus.Spoilable;
                }
                else
                {
                    // if different location or date then provisional ballot
                    voterStatus = VoterLookupStatus.Provisional;
                }
            }
            else if (!voter.IsValidPollId(pollId))
            {
                voterStatus = VoterLookupStatus.Hybrid;
            }
            // Check if voter has a valid ballot style
            else if (!voter.IsEligible())
            {
                //IneligibleVoter
                voterStatus = VoterLookupStatus.Ineligible;
            }

            // Add precinct check for Hybrid sites (set voterStatus = VoterLookupStatus.Provisional when not for current site)
            // Voter gets official ballot if they exist in ElectionPrecinctPoll
            // Other wise they get a provisional

            else
            {
                // Allow user to procceed to print out a ballot
                voterStatus = VoterLookupStatus.Eligible;
            }

            return voterStatus;
        }

    }
}
