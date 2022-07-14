using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.SystemSettings;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Reports
{
    public interface IReport
    {
        string Name { get; set; }                       // Name displayed in the report list
        string File { get; set; }                       // File name of the report
        string Source { get; set; }                     // Data source inside the report
        ReportOrientation Orientation { get; set; }     // Sets the width of the report preview screen
        bool Daily { get; set; }                        // Sets the report as daily only

        // There is no need to have both sets of methods
        // Just make sure to include the current site on the VCC reports

        // VCC Reports
        //string PrintReport(SystemSettingsController settings);
        //string PrintReport(SystemSettingsController settings, DateTime reportingDate);
        //string PrintReport(SystemSettingsController settings, DateTime startDate, DateTime endDate);
        //string PreviewReport(SystemSettingsController settings);
        //string PreviewReport(SystemSettingsController settings, DateTime reportingDate);
        //string PreviewReport(SystemSettingsController settings, DateTime startDate, DateTime endDate);

        // ABS Reports
        string PrintReport(SystemSettingsController settings, int? pollId, bool reportPrinter);
        string PrintReport(SystemSettingsController settings, int? pollId, DateTime reportingDate, bool reportPrinter);
        string PrintReport(SystemSettingsController settings, int? pollId, DateTime startDate, DateTime endDate, bool reportPrinter);
        string PreviewReport(SystemSettingsController settings, int? pollId);
        string PreviewReport(SystemSettingsController settings, int? pollId, DateTime reportingDate);
        string PreviewReport(SystemSettingsController settings, int? pollId, DateTime startDate, DateTime endDate);

        string PrintReport(SystemSettingsController settings, string location, bool reportPrinter);
        string PrintReport(SystemSettingsController settings, string location, DateTime reportingDate, bool reportPrinter);
        string PrintReport(SystemSettingsController settings, string location, DateTime startDate, DateTime endDate, bool reportPrinter);
        string PreviewReport(SystemSettingsController settings, string location);
        string PreviewReport(SystemSettingsController settings, string location, DateTime reportingDate);
        string PreviewReport(SystemSettingsController settings, string location, DateTime startDate, DateTime endDate);
    }
}
