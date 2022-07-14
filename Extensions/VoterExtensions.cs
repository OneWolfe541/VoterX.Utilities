using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Elections;
using VoterX.Core.Voters;
using VoterX.SystemSettings;
using VoterX.SystemSettings.Models;

namespace VoterX.Utilities.Extensions
{
    public static class VoterExtensions
    {
        public static void Localize(this NMVoter voter, GlobalSettingsModel settings)
        {
            voter.Localize(
                settings.Election.ElectionID,
                settings.User.UserID,
                settings.User.UserName,
                settings.System.SiteID ?? 0,
                settings.System.SiteName,
                settings.System.MachineID);
        }

        public static void Localize(this NMVoter voter, SystemSettingsController settings)
        {
            voter.Localize(
                settings.Election.ElectionID,
                settings.User.UserID,
                settings.User.UserName,
                settings.System.SiteID ?? 0,
                settings.System.SiteName,
                settings.System.MachineID);
        }

        public static void SetBallotStyle(this NMVoter voter, BallotStyleModel style)
        {
            voter.Data.BallotStyleID = style.BallotStyleId;
            voter.Data.BallotStyle = style.BallotStyleName;
            voter.Data.BallotStyleFile = style.BallotStyleFileName;
        }
    }
}
