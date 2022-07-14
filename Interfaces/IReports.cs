using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.SystemSettings;

namespace VoterX.Utilities.Interfaces
{
    public interface IReports
    {
        string ReportName { get; set; }

        string PrintReport(int electionID, int pollID, SystemSettingsController settings);

        string PrintReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings);

        string PrintReport(int electionID, int pollID, DateTime startDate, DateTime endDate, SystemSettingsController settings);

        string PreviewReport(int electionID, int pollID, SystemSettingsController settings);

        string PreviewReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings);

        string PreviewReport(int electionID, int pollID, DateTime startDate, DateTime endDate, SystemSettingsController settings);
    }
}
