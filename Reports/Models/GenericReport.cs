using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.SystemSettings;
using VoterX.Utilities.Methods;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Reports
{
    public class GenericReport : IReport
    {
        public string Name { get; set; }
        public string File { get; set; }
        public string Source { get; set; }
        public string SQL { get; set; }
        public ReportOrientation Orientation { get; set; }
        public bool Daily { get; set; }
        public bool Debug { get; set; }
        public bool Absentee { get; set; }
        public bool Log { get; set; }

        public GenericReport()
        {

        }

        public GenericReport(string name, string file, string source, string sql, ReportOrientation orientation, bool? debug) : this(name, file, source, sql, orientation, debug, null, null) { }
        public GenericReport(string name, string file, string source, string sql, ReportOrientation orientation, bool? debug, bool? daily, bool? absentee) : this(name, file, source, sql, orientation, debug, daily, absentee, null) { }
        public GenericReport(string name, string file, string source, string sql, ReportOrientation orientation, bool? debug, bool? daily, bool? absentee, bool? log)
        {
            Name = name;
            File = file;
            Source = source;
            SQL = sql;
            Orientation = orientation;
            Daily = daily ?? false;
            Debug = debug ?? false;
            Absentee = absentee ?? false;
            Log = log ?? true;
        }

        #region PrintReport
        public string PrintReport(SystemSettingsController settings, int? pollId, bool reportPrinter)
        {
            string sqlDetails = Query(settings, pollId);

            string reportType = "Activity to Date";
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
            }
            else
            {
                return "Cannot print all activity for a daily report!";
            }
        }

        public string PrintReport(SystemSettingsController settings, int? pollId, DateTime reportingDate, bool reportPrinter)
        {
            string sqlDetails = Query(settings, pollId, reportingDate);

            string reportType = "Activity for " + reportingDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
        }

        public string PrintReport(SystemSettingsController settings, int? pollId, DateTime startDate, DateTime endDate, bool reportPrinter)
        {
            string sqlDetails = Query(settings, pollId, startDate, endDate);

            string reportType = "Activity from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
            }
            else
            {
                return "Cannot print a date range for a daily report!";
            }
        }
        #endregion

        #region PrintReportLocationAsString
        public string PrintReport(SystemSettingsController settings, string location, bool reportPrinter)
        {
            string sqlDetails = Query(settings, location);

            string reportType = "Activity to Date";
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
            }
            else
            {
                return "Cannot print all activity for a daily report!";
            }
        }

        public string PrintReport(SystemSettingsController settings, string location, DateTime reportingDate, bool reportPrinter)
        {
            string sqlDetails = Query(settings, location, reportingDate);

            string reportType = "Activity for " + reportingDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
        }

        public string PrintReport(SystemSettingsController settings, string location, DateTime startDate, DateTime endDate, bool reportPrinter)
        {
            string sqlDetails = Query(settings, location, startDate, endDate);

            string reportType = "Activity from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PrintSilentReport(File, settings, sqlDetails, sqlHeader, Debug, Source, reportPrinter);
            }
            else
            {
                return "Cannot print a date range for a daily report!";
            }
        }
        #endregion

        #region ReviewReport
        public string PreviewReport(SystemSettingsController settings, int? pollId)
        {
            string sqlDetails = Query(settings, pollId);

            string reportType = "Activity to Date";
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
            }
            else
            {
                return "Cannot print all activity for a daily report!";
            }
        }

        public string PreviewReport(SystemSettingsController settings, int? pollId, DateTime reportingDate)
        {
            string sqlDetails = Query(settings, pollId, reportingDate);

            string reportType = "Activity for " + reportingDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
        }

        public string PreviewReport(SystemSettingsController settings, int? pollId, DateTime startDate, DateTime endDate)
        {
            string sqlDetails = Query(settings, pollId, startDate, endDate);

            string reportType = "Activity from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
            }
            else
            {
                return "Cannot print a date range for a daily report!";
            }
        }
        #endregion

        #region PreviewReportLocationAsString
        public string PreviewReport(SystemSettingsController settings, string location)
        {
            string sqlDetails = Query(settings, location);

            string reportType = "Activity to Date";
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
            }
            else
            {
                return "Cannot print all activity for a daily report!";
            }
        }

        public string PreviewReport(SystemSettingsController settings, string location, DateTime reportingDate)
        {
            string sqlDetails = Query(settings, location, reportingDate);

            string reportType = "Activity for " + reportingDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
        }

        public string PreviewReport(SystemSettingsController settings, string location, DateTime startDate, DateTime endDate)
        {
            string sqlDetails = Query(settings, location, startDate, endDate);

            string reportType = "Activity from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
            string sqlHeader = ReportQueries.ElectionDetails(settings.Election.ElectionID, reportType, settings.System.SiteID);

            if (Daily == false)
            {
                return FastReportsMethods.PreviewReport(File, sqlDetails, sqlHeader, Debug, Source, settings, Orientation, Absentee);
            }
            else
            {
                return "Cannot print a date range for a daily report!";
            }
        }
        #endregion

        #region Query
        private string Query(SystemSettingsController settings, int? pollId)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            //sql += "WHERE ";
            if (pollId != null) sql += "WHERE PollId = " + pollId + " ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";

            return sql;
        }

        private string Query(SystemSettingsController settings, int? pollId, DateTime reportingDate)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            sql += "WHERE ";
            if (pollId != null) sql += "PollId = " + pollId + " AND ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";
            // Specific date
            sql += "CAST(ActivityDate AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return sql;
        }

        private string Query(SystemSettingsController settings, int? pollId, DateTime startDate, DateTime endDate)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            sql += "WHERE ";
            if (pollId != null) sql += "PollId = " + pollId + " AND ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";
            // Given date range
            sql += "ActivityDate >= '" + startDate.ToShortDateString() + "' ";
            sql += "AND ActivityDate <'" + endDate.AddDays(1).ToShortDateString() + "' ";

            return sql;
        }
        #endregion

        #region QueryLocationAsString
        private string Query(SystemSettingsController settings, string location)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            //sql += "WHERE ";
            if (location != null) sql += "WHERE BinName = '" + location + "' ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";

            return sql;
        }

        private string Query(SystemSettingsController settings, string location, DateTime reportingDate)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            sql += "WHERE ";
            if (location != null) sql += "BinName = '" + location + "' AND ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";
            // Specific date
            sql += "CAST(ActivityDate AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return sql;
        }

        private string Query(SystemSettingsController settings, string location, DateTime startDate, DateTime endDate)
        {
            string sql = "SELECT * ";
            sql += "FROM (" + SQL + ") AS vDailyDetailReport ";
            sql += "WHERE ";
            if (location != null) sql += "BinName = '" + location + "' AND ";
            //sql += "ElectionId = " + settings.Election.ElectionID + " ";
            // Given date range
            sql += "ActivityDate >= '" + startDate.ToShortDateString() + "' ";
            sql += "AND ActivityDate <'" + endDate.AddDays(1).ToShortDateString() + "' ";

            return sql;
        }
        #endregion
    }
}
