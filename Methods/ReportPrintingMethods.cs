using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Core.Voters;
using VoterX.SystemSettings;
using VoterX.Core.Extensions;
using VoterX.Utilities.Methods.ReportSubQueries;
using VoterX.SystemSettings.Enums;
using System.IO;

namespace VoterX.Utilities.Methods
{
    public static class ReportPrintingMethods
    {
        //private static bool debugMode = false;

        public static string PrintVoterApplication(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.DayOfWeek.ToString() + ", " + electionDay.ToLongDateString() + "', ";

            // Check if signature folder exists
            if (Directory.Exists(settings.System.SignatureFolder))
            {
                sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            }
            else
            {
                // Use local signature folder
                sql += "signature_path = 'C:\\AutoVote\\Signatures\\" + voter.VoterID.ToString() + ".jpg', ";
            }

            sql += "ballot_style_name = '" + voter.BallotStyle + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Early Voter', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";            

            return FastReportsMethods.PrintSilentReport("Application", settings, sql, settings.GetDebugMode());
        }

        public static string PrintVoterPermit(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            if (voter.BallotPrinted == null)
            {
                sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), "; // Change this to printed date
            }
            else
            {
                sql += "printed_date = CONVERT(DATETIME,'" + voter.BallotPrinted.ToString() + "'), ";
            }
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle.ReplaceApostrophe() + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Election Day', ";
            sql += "spoiled_ballot = 'FALSE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentReport("Permit", settings, sql, settings.GetDebugMode());
        }

        public static string PrintVoterPermitSpoiled(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            //sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            if (voter.BallotPrinted == null)
            {
                sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), "; // Change this to printed date
            }
            else
            {
                sql += "printed_date = CONVERT(DATETIME,'" + voter.BallotPrinted.ToString() + "'), ";
            }
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle.ReplaceApostrophe() + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Election Day', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentReport("Permit", settings, sql, settings.GetDebugMode());
        }

        public static string PrintBallotStub(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.DayOfWeek.ToString() + ", " + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle.ReplaceApostrophe() + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Early Voter', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentReport("Stub", settings, sql, settings.GetDebugMode());
        }

        public static string PrintSignatureForm(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle.ReplaceApostrophe() + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Election Day', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentReport("SignatureForm", settings, sql, settings.GetDebugMode());
        }

        public static string PrintAbsenteeAffidavit(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT Elections.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.DayOfWeek.ToString() + ", " + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Early Voter', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM Elections WHERE ElectionId = " + settings.Election.ElectionID + " AND CountyCode = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentReport("Affidavit", settings, sql, settings.GetDebugMode());
        }

        public static string PrintDailyDetailBCReport(int pollID, DateTime date, SystemSettingsController settings)
        {
            string sql = "SELECT * FROM  vDailyDetailReportPrimary ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "CAST([date_voted] AS DATE) = CAST(CONVERT(DATETIME,'" + date + "') AS DATE) ";

            return FastReportsMethods.PrintSilentReport("DailyDetailReportBC_Primary01", settings, sql, settings.GetDebugMode(), "DailyDetailData");
        }

        public static string PrintDailyDetailBSReport(int pollID, DateTime date, SystemSettingsController settings)
        {
            string sql = "SELECT *, ";
            if (settings.System.VCCType == VotingCenterMode.EarlyVoting)
            {
                sql += "ReportType = 'Early Voting' ";
            }
            else
            {
                sql += "ReportType = 'Election Day' ";
            }
            // Replaced stored Views with inline SQL 8/23/2018
            //sql += "FROM vDailyDetailReport ";
            sql += "FROM (" + ReportViews.DailyDetailReportView() + ") AS vDailyDetailReport ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "CAST([date_voted] AS DATE) = CAST(CONVERT(DATETIME,'" + date + "') AS DATE) ";

            return FastReportsMethods.PrintSilentReport("DailyDetailReportBS_General03", settings, sql, settings.GetDebugMode(), "DailyDetailData");
        }

        public static string PrintDailySummaryReport(DateTime date, SystemSettingsController settings)
        {
            string sql = "SELECT *, ";
            if (settings.System.VCCType == VotingCenterMode.EarlyVoting)
            {
                sql += "VotingType = 'Early Voting' ";
            }
            else
            {
                sql += "VotingType = 'Election Day' ";
            }
            // Replaced stored Views with inline SQL 8/23/2018
            //sql += "FROM vSiteSummaryByDate ";
            sql += "FROM (" + ReportViews.SiteSummaryByDateView() + ") AS vSiteSummaryByDate ";
            sql += "WHERE ";
            sql += "[poll_id] = " + settings.System.SiteID + " AND ";
            sql += "election_id = " + settings.Election.ElectionID + " AND ";
            sql += "CAST([date_voted] AS DATE) = CAST(CONVERT(DATETIME,'" + date + "') AS DATE) ";

            return FastReportsMethods.PrintSilentReport("DailySummaryReportBS03", settings, sql, settings.GetDebugMode(), "SummaryReportData");
        }

        public static string PrintZeroEarlyVotingBSReport(SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            // Replaced stored Views with inline SQL 8/23/2018
            //sql += "FROM vSiteSummary ";
            sql += "FROM (" + ReportViewsRevised.SiteSummaryView() + ") AS vSiteSummaryByDate ";
            sql += "WHERE ";
            sql += "LogCode = 8 AND ";
            sql += "PollId = " + settings.System.SiteID + " AND ";
            sql += "ElectionId = " + settings.Election.ElectionID + " ";

            return FastReportsMethods.PrintSilentReport("ZeroReportEarlyVoting", settings, sql, settings.GetDebugMode(), "ZeroReportData");
        }

        public static string PrintZeroElectionDayReport(SystemSettingsController settings)
        {
            // Replaced stored Views with inline SQL 8/23/2018
            //string sql = "SELECT * FROM vSiteSummaryByDate ";
            string sql = "SELECT * ";
            sql += "FROM (" + ReportViewsRevised.SiteSummaryByDateView() + ") AS vSiteSummaryByDate ";
            sql += "WHERE ";
            sql += "LogCode = 16 AND ";
            sql += "PollId = " + settings.System.SiteID + " AND ";
            sql += "ElectionId = " + settings.Election.ElectionID + " ";
            //sql += "AND CAST(date_voted AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("ZeroReportElectionDay", settings, sql, settings.GetDebugMode(), "ZeroReportData");
        }

        public static string PrintZeroReport(int electionID, int pollID, SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            if (settings.System.VCCType == VotingCenterMode.EarlyVoting)
            {
                sql += ",ReportType = 'Early Voting' ";
            }
            else if (settings.System.VCCType == VotingCenterMode.ElectionDay)
            {
                sql += ",ReportType = 'Election Day' ";
            }
            sql += "FROM vSiteSummaryByDate ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(date_voted AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("ZeroReportBySite03", settings, sql, settings.GetDebugMode(), "ZeroReportData");
        }

        public static string PrintZeroReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            if (settings.System.VCCType == VotingCenterMode.EarlyVoting)
            {
                sql += ",ReportType = 'Early Voting' ";
            }
            else if (settings.System.VCCType == VotingCenterMode.ElectionDay)
            {
                sql += ",ReportType = 'Election Day' ";
            }
            sql += "FROM vSiteSummaryByDate ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(date_voted AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("ZeroReportBySite03", settings, sql, settings.GetDebugMode(), "ZeroReportData");
        }

        public static string PrintDailyProvisionalReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            //string sql = "SELECT * FROM vDailyProvisionalReport ";
            // Replaced stored Views with inline SQL 8/23/2018
            sql += "FROM (" + ReportViews.DailyProvisionalReportView() + ") AS vDailyProvisionalReport ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(last_modified AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("DailyProvisionalReportBS01", settings, sql, settings.GetDebugMode(), "DailyProvisionalData");
        }

        public static string PrintDailySpoiledPrimaryReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * FROM vDailySpoiledReportPrimary ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(printed_date AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("DailySpoiledReportBS_Primary01", settings, sql, settings.GetDebugMode(), "DailySpoiledData");
        }

        public static string PrintDailySpoiledReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            //string sql = "SELECT * FROM vDailySpoiledReport ";
            // Replaced stored Views with inline SQL 8/23/2018
            sql += "FROM (" + ReportViews.DailySpoiledReportView() + ") AS vDailySpoiledReport ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(printed_date AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("DailySpoiledReportBS_General01", settings, sql, settings.GetDebugMode(), "DailySpoiledData");
        }

        public static string PrintEndOfDayReport(DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * ";
            //string sql = "SELECT * FROM vEndOfDayReport ";
            // Replaced stored Views with inline SQL 8/23/2018
            sql += "FROM (" + ReportViews.EndOfDayReportView() + ") AS vEndOfDayReport ";
            sql += "WHERE ";
            sql += "poll_id = " + settings.System.SiteID + " AND ";
            sql += "poll_id = " + settings.System.SiteID + " AND ";
            sql += "county_code = " + settings.Election.CountyCode + " AND ";
            sql += "election_id = " + settings.Election.ElectionID + " ";
            sql += "AND CAST(poll_date AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            string reportfile;
            if (settings.System.VCCType == VotingCenterMode.EarlyVoting)
            {
                reportfile = "EndOfDayReportEV01";
            }
            else
            {
                reportfile = "EndOfDayReportED01";
            }

            return FastReportsMethods.PrintSilentReport(reportfile, settings, sql, settings.GetDebugMode(), "vEndOfDayReport");
        }

        public static string PrintEarlyVotingEndOfDayReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * FROM vEndOfDayReport ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(poll_date AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("EndOfDayReportEV01", settings, sql, settings.GetDebugMode(), "vEndOfDayReport");
        }

        public static string PrintElectionDayEndOfDayReport(int electionID, int pollID, DateTime reportingDate, SystemSettingsController settings)
        {
            string sql = "SELECT * FROM vEndOfDayReport ";
            sql += "WHERE ";
            sql += "[poll_id] = " + pollID + " AND ";
            sql += "election_id = " + electionID + " ";
            sql += "AND CAST(poll_date AS DATE) = CONVERT(VARCHAR(10), CONVERT(DATETIME,'" + reportingDate.ToString() + "'), 101) ";

            return FastReportsMethods.PrintSilentReport("EndOfDayReportED01", settings, sql, settings.GetDebugMode(), "vEndOfDayReport");
        }

        public static string PrintTrainingBallot(VoterDataModel voter, SystemSettingsController settings)
        {
            string sql = "SELECT tblElection.*, ";
            sql += "voter_id = " + voter.VoterID.ToString() + ", ";
            sql += "full_name = '" + voter.FullName.ReplaceApostrophe() + "', ";
            sql += "birth_year = " + voter.DOBYear + ", ";
            sql += "printed_date = CONVERT(DATETIME,'" + DateTime.Now.ToString() + "'), ";
            sql += "address = '" + voter.Address1.ReplaceApostrophe() + "', ";
            sql += "city_state_zip = '" + voter.City.ReplaceApostrophe() + ", " + voter.State + " " + voter.Zip + "', ";
            var electionDay = settings.Election.ElectionDate;
            sql += "election_date_long = '" + electionDay.DayOfWeek.ToString() + ", " + electionDay.ToLongDateString() + "', ";
            sql += "signature_path = '" + settings.System.SignatureFolder + "\\" + voter.VoterID.ToString() + ".jpg', ";
            sql += "ballot_style_name = '" + voter.BallotStyle + "', ";
            sql += "election_entity = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "jurisdiction = '" + voter.PrecinctName.ReplaceApostrophe() + "', ";
            sql += "county_name = '" + settings.Election.ElectionEntity.ReplaceApostrophe() + "', ";
            sql += "poll_id = " + settings.System.SiteID + ", ";
            sql += "place_name = '" + settings.System.SiteName.RemoveSpecialCharacters() + "', ";
            sql += "computer = " + settings.System.MachineID + ", ";
            sql += "vccmode = 'Early Voter', ";
            sql += "spoiled_ballot = 'TRUE', ";
            sql += "ballot_number = " + (voter.BallotNumber == null ? "NULL" : voter.BallotNumber.ToString().ReplaceApostrophe()) + " ";
            sql += "FROM tblElection WHERE [election_id] = " + settings.Election.ElectionID + " AND [county_code] = '" + settings.Election.CountyCode + "'";

            return FastReportsMethods.PrintSilentBallot("BallotReport01", settings, sql, settings.GetDebugMode());
        }
    }
}
