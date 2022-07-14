using AutoVote.Logging;
using VoterX.SystemSettings;
using VoterX.SystemSettings.Models;
using VoterX.Utilities.Dialogs;
using VoterX.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Methods
{
    public static class FastReportsMethods
    {
        // Change connection strings
        // https://www.fast-report.com/en/forum/index.php?showtopic=5332
        // https://www.fast-report.com/en/forum/index.php?showtopic=4778

        //public static void PreviewReport(string reportFileName)
        //{
        //    ReportPreviewWindow reportPreview = new ReportPreviewWindow(reportFileName);
        //    reportPreview.ShowDialog();
        //}

        public static string PreviewReport(string reportFileName, string sql, bool useDefaultConnection, string dataSource, SystemSettingsController settings)
        {
            string message = "";
            try
            {
                ReportPreviewWindow reportPreview = new ReportPreviewWindow(reportFileName, sql, useDefaultConnection, dataSource, settings);
                reportPreview.ShowDialog();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        public static string PreviewReport(string reportFileName, string sqlDetail, string sqlHeader, bool useDefaultConnection, string dataSource, SystemSettingsController settings)
        {
            return PreviewReport(reportFileName, sqlDetail, sqlHeader, useDefaultConnection, dataSource, settings, null, null);
        }
        public static string PreviewReport(string reportFileName, string sqlDetail, string sqlHeader, bool useDefaultConnection, string dataSource, SystemSettingsController settings, ReportOrientation? orientation, bool? absentee)
        {
            string message = "";
            try
            {
                ReportPreviewWindow reportPreview = new ReportPreviewWindow(reportFileName, sqlDetail, sqlHeader, useDefaultConnection, dataSource, settings, absentee);
                reportPreview.SetOrientation(orientation);
                reportPreview.ShowDialog();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return message;
        }

        //private static int SetOrientation(ReportOrientation orientation)
        //{
        //    int width = 850;
        //    switch(orientation)
        //    {
        //        case ReportOrientation.Portrait:
        //            width = 870;
        //            break;
        //        case ReportOrientation.Landscape:
        //            width = 1200;
        //            break;
        //    }
        //    return width;
        //}

        public static string PrintSilentReport(string reportFileName, SystemSettingsController system)
        {
            string message = "";
            string reportPath = (system.Reports.ReportsFolder + "\\" + reportFileName + ".frx");

            AutoVoteLogger reportLogger = new AutoVoteLogger("FastReports", system.System.ReportErrorLogging);
            reportLogger.WriteLog("Loading Report Print File: " + reportPath);

            FastReport.Report report1 = new FastReport.Report();

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                reportLogger.WriteLog("File Not Found: " + reportPath);
                message = "File Not Found";
            }

            // https://www.fast-report.com/en/forum/index.php?showtopic=14341
            report1.PrintSettings.Printer = system.Printers.ApplicationPrinter;
            report1.PrintSettings.PaperSource = system.Printers.AppBin;

            // Hide the progress boxes
            // https://stackoverflow.com/questions/30937179/how-to-print-fastreport-directly-without-showing-any-dialog-in-my-application
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;

            // https://www.fast-report.com/en/forum/index.php?showtopic=5023
            if (report1.Prepare())
            {
                report1.PrintSettings.ShowDialog = false;
                report1.PrintPrepared();
            }            

            return message;
        }

        // https://www.fast-report.com/en/forum/index.php?showtopic=14434
        public static string PrintSilentReport(string reportFileName, SystemSettingsController system, string sql, bool useDefaultConnection)
        {
            string message = "";
            string reportPath = (system.Reports.ReportsFolder + "\\" + reportFileName + ".frx");

            AutoVoteLogger reportLogger = new AutoVoteLogger("FastReports", system.System.ReportErrorLogging);
            reportLogger.WriteLog("Loading Report Print File: " + reportPath);
            reportLogger.WriteLog("Report SQL: " + sql);

            FastReport.Report report1 = new FastReport.Report();

            // https://www.fast-report.com/en/forum/index.php?showtopic=14341
            //report1.PrintSettings.Printer = AppSettings.Printers.ApplicationPrinter;
            //report1.PrintSettings.PaperSource = AppSettings.Printers.AppBin;

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                reportLogger.WriteLog("File Not Found: " + reportPath);
                return "File Not Found: " + reportPath;
            }

            if (useDefaultConnection == false)
            {
                try
                {
                    //// Change connection string
                    string OldConnectionString = report1.Dictionary.Connections[0].ConnectionString;
                    string newConnectionString = "data source=AESSQL2;initial catalog=SOSDataExchange;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                    //newConnectionString = "Data Source=" + AppSettings.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + AppSettings.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=InkImpr35510n5";
                    newConnectionString = "Data Source=" + system.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + system.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=@ESf13lddba";
                    report1.Dictionary.Connections[0].ConnectionString = newConnectionString;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    reportLogger.WriteLog("Connection Error: " + e.Message.ToString());
                    return "Connection Error: ";
                }
            }

            // Get data source from report
            FastReport.Data.TableDataSource tds = (FastReport.Data.TableDataSource)report1.GetDataSource("ApplicationData");

            // Save query to data source
            tds.SelectCommand = sql;

            try
            {
                // https://www.fast-report.com/en/forum/index.php?showtopic=14341
                report1.PrintSettings.Printer = system.Printers.ApplicationPrinter;
                report1.PrintSettings.PaperSource = system.Printers.AppBin;

                // Hide the progress boxes
                // https://stackoverflow.com/questions/30937179/how-to-print-fastreport-directly-without-showing-any-dialog-in-my-application
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;

                // https://www.fast-report.com/en/forum/index.php?showtopic=5023
                if (report1.Prepare())
                {
                    // Do Stuff
                }
            }
            catch (Exception e)
            {
                var connection = report1.Dictionary.Connections[0];
                //return "Cannot Prepare: " + connection.GetConnection().ConnectionString;
                reportLogger.WriteLog("Cannot Prepare: " + e.Message.ToString());
                return "Cannot Prepare: " + e.Message;
            }

            try
            {
                report1.PrintSettings.ShowDialog = false;
                report1.PrintPrepared();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                reportLogger.WriteLog("Cannot Print: " + e.Message.ToString());
                return "Cannot Print: ";
            }

            return message;
            //return report1.PrintSettings.Printer.ToString();
        }

        public static string PrintSilentReport(string reportFileName, SystemSettingsController system, string sql, bool useDefaultConnection, string dataSource)
        {
            string message = "";
            string reportPath = (system.Reports.ReportsFolder + "\\" + reportFileName + ".frx");

            AutoVoteLogger reportLogger = new AutoVoteLogger("FastReports", system.System.ReportErrorLogging);
            reportLogger.WriteLog("Loading Report Print File: " + reportPath);
            reportLogger.WriteLog("Report SQL: " + sql);

            FastReport.Report report1 = new FastReport.Report();

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                reportLogger.WriteLog("File Not Found: " + reportPath);
                return "File Not Found: " + reportPath;
            }

            if (useDefaultConnection == false)
            {
                try
                {
                    //// Change connection string
                    string OldConnectionString = report1.Dictionary.Connections[0].ConnectionString;
                    string newConnectionString = "data source=AESSQL2;initial catalog=SOSDataExchange;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                    //newConnectionString = "Data Source=" + AppSettings.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + AppSettings.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=InkImpr35510n5";
                    newConnectionString = "Data Source=" + system.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + system.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=@ESf13lddba";
                    report1.Dictionary.Connections[0].ConnectionString = newConnectionString;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    reportLogger.WriteLog("Connection Error: " + e.Message.ToString());
                    return "Connection Error: ";
                }
            }

            // Get data source from report
            FastReport.Data.TableDataSource tds = (FastReport.Data.TableDataSource)report1.GetDataSource(dataSource);

            // Save query to data source
            tds.SelectCommand = sql;

            try
            {
                // https://www.fast-report.com/en/forum/index.php?showtopic=14341
                report1.PrintSettings.Printer = system.Printers.ApplicationPrinter;
                report1.PrintSettings.PaperSource = system.Printers.AppBin;

                // Hide the progress boxes
                // https://stackoverflow.com/questions/30937179/how-to-print-fastreport-directly-without-showing-any-dialog-in-my-application
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;

                // https://www.fast-report.com/en/forum/index.php?showtopic=5023
                if (report1.Prepare())
                {
                    // Do Stuff
                }
            }
            catch (Exception e)
            {
                var connection = report1.Dictionary.Connections[0];
                //return "Cannot Prepare: " + connection.GetConnection().ConnectionString;
                reportLogger.WriteLog("Cannot Prepare: " + e.Message.ToString());
                return "Cannot Prepare: " + e.Message;
            }

            try
            {
                report1.PrintSettings.ShowDialog = false;
                report1.PrintPrepared();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                reportLogger.WriteLog("Cannot Print: " + e.Message.ToString());
                return "Cannot Print: ";
            }

            return message;
            //return report1.PrintSettings.Printer.ToString();
        }

        public static string PrintSilentReport(string reportFileName, SystemSettingsController system, string sqlDetail, string sqlHeader, bool useDefaultConnection, string dataSource, bool reportPrinter)
        {
            return PrintSilentReport(reportFileName, system, sqlDetail, sqlHeader, useDefaultConnection, dataSource, reportPrinter, true);
        }

        public static string PrintSilentReport(string reportFileName, SystemSettingsController system, string sqlDetail, string sqlHeader, bool useDefaultConnection, string dataSource, bool reportPrinter, bool log)
        {
            string message = "";
            string reportPath = (system.Reports.ReportsFolder + "\\" + reportFileName + ".frx");

            AutoVoteLogger reportLogger = new AutoVoteLogger("FastReports", system.System.ReportErrorLogging);
            reportLogger.WriteLog("Loading Report Print File: " + reportPath);
            if (log == true)
            {
                reportLogger.WriteLog("Report SQL: " + sqlDetail);
            }
            if(sqlHeader != null && sqlHeader != "" && log == true)
            {
                reportLogger.WriteLog("Header SQL: " + sqlHeader);
            }

            FastReport.Report report1 = new FastReport.Report();

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                reportLogger.WriteLog("File Not Found: " + reportPath);
                return "File Not Found: " + reportPath;
            }

            if (useDefaultConnection == false)
            {
                try
                {
                    //// Change connection string
                    string OldConnectionString = report1.Dictionary.Connections[0].ConnectionString;
                    string newConnectionString = "data source=AESSQL2;initial catalog=SOSDataExchange;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                    //newConnectionString = "Data Source=" + AppSettings.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + AppSettings.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=InkImpr35510n5";
                    newConnectionString = "Data Source=" + system.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + system.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=@ESf13lddba";

                    report1.Dictionary.Connections[0].ConnectionString = newConnectionString;
                    //reportLogger.WriteLog("Connection String 1: " + report1.Dictionary.Connections[0].ConnectionString);

                    // Check if a second connection exists
                    try
                    {
                        report1.Dictionary.Connections[1].ConnectionString = newConnectionString;
                        //reportLogger.WriteLog("Connection String 2: " + report1.Dictionary.Connections[1].ConnectionString);
                    }
                    catch { }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    reportLogger.WriteLog("Connection Error: " + e.Message.ToString());
                    return "Connection Error: ";
                }
            }
            else
            {
                //reportLogger.WriteLog("Connection String 1: " + report1.Dictionary.Connections[0].ConnectionString);

                try
                {
                    //reportLogger.WriteLog("Connection String 2: " + report1.Dictionary.Connections[1].ConnectionString);
                }
                catch { }
            }

            // Get data source from report
            FastReport.Data.TableDataSource tds = (FastReport.Data.TableDataSource)report1.GetDataSource(dataSource);

            FastReport.Data.TableDataSource tdsHeader = (FastReport.Data.TableDataSource)report1.GetDataSource("ElectionData");

            // Save query to data source
            tds.SelectCommand = sqlDetail;

            if (tdsHeader != null) tdsHeader.SelectCommand = sqlHeader;

            try
            {
                if (reportPrinter == false)
                {
                    // https://www.fast-report.com/en/forum/index.php?showtopic=14341
                    report1.PrintSettings.Printer = system.Printers.ApplicationPrinter;
                    report1.PrintSettings.PaperSource = system.Printers.AppBin;
                }
                else
                {
                    report1.PrintSettings.Printer = system.Printers.ReportPrinter;
                    report1.PrintSettings.PaperSource = system.Printers.ReportBin;
                }

                // Hide the progress boxes
                // https://stackoverflow.com/questions/30937179/how-to-print-fastreport-directly-without-showing-any-dialog-in-my-application
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;

                // https://www.fast-report.com/en/forum/index.php?showtopic=5023
                if (report1.Prepare())
                {
                    // Do Stuff
                }
            }
            catch (Exception e)
            {
                var connection = report1.Dictionary.Connections[0];
                //return "Cannot Prepare: " + connection.GetConnection().ConnectionString;
                reportLogger.WriteLog("Cannot Prepare: " + e.Message.ToString());
                return "Cannot Prepare: " + e.Message;
            }

            try
            {
                report1.PrintSettings.ShowDialog = false;
                report1.PrintPrepared();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                reportLogger.WriteLog("Cannot Print: " + e.Message.ToString());
                return "Cannot Print: ";
            }

            return message;
            //return report1.PrintSettings.Printer.ToString();
        }

        public static string PrintSilentBallot(string reportFileName, SystemSettingsController system, string sql, bool useDefaultConnection)
        {
            string message = "";
            string reportPath = (system.Reports.ReportsFolder + "\\" + reportFileName + ".frx");

            AutoVoteLogger reportLogger = new AutoVoteLogger("FastReports", system.System.ReportErrorLogging);
            reportLogger.WriteLog("Loading Report Print File: " + reportPath);
            reportLogger.WriteLog("Report SQL: " + sql);

            FastReport.Report report1 = new FastReport.Report();

            // https://www.fast-report.com/en/forum/index.php?showtopic=14341
            //report1.PrintSettings.Printer = AppSettings.Printers.ApplicationPrinter;
            //report1.PrintSettings.PaperSource = AppSettings.Printers.AppBin;

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                reportLogger.WriteLog("File Not Found: " + reportPath);
                return "File Not Found: " + reportPath;
            }

            if (useDefaultConnection == false)
            {
                try
                {
                    //// Change connection string
                    string OldConnectionString = report1.Dictionary.Connections[0].ConnectionString;
                    string newConnectionString = "data source=AESSQL2;initial catalog=SOSDataExchange;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                    //newConnectionString = "Data Source=" + AppSettings.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + AppSettings.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=InkImpr35510n5";
                    newConnectionString = "Data Source=" + system.Network.SQLServer + ";AttachDbFilename=;Initial Catalog=" + system.Network.LocalDatabase + ";Integrated Security=False;Persist Security Info=True;User ID=sa;Password=@ESf13lddba";
                    report1.Dictionary.Connections[0].ConnectionString = newConnectionString;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    reportLogger.WriteLog("Connection Error: " + e.Message.ToString());
                    return "Connection Error: ";
                }
            }

            // Get data source from report
            FastReport.Data.TableDataSource tds = (FastReport.Data.TableDataSource)report1.GetDataSource("ApplicationData");

            // Save query to data source
            tds.SelectCommand = sql;

            try
            {
                // https://www.fast-report.com/en/forum/index.php?showtopic=14341
                report1.PrintSettings.Printer = system.Printers.BallotPrinter;
                report1.PrintSettings.PaperSource = system.Printers.BallotBin;

                // Hide the progress boxes
                // https://stackoverflow.com/questions/30937179/how-to-print-fastreport-directly-without-showing-any-dialog-in-my-application
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;

                // https://www.fast-report.com/en/forum/index.php?showtopic=5023
                if (report1.Prepare())
                {
                    // Do Stuff
                }
            }
            catch (Exception e)
            {
                var connection = report1.Dictionary.Connections[0];
                //return "Cannot Prepare: " + connection.GetConnection().ConnectionString;
                reportLogger.WriteLog("Cannot Prepare: " + e.Message.ToString());
                return "Cannot Prepare: " + e.Message;
            }

            try
            {
                report1.PrintSettings.ShowDialog = false;
                report1.PrintPrepared();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                reportLogger.WriteLog("Cannot Print: " + e.Message.ToString());
                return "Cannot Print: ";
            }

            return message;
            //return report1.PrintSettings.Printer.ToString();
        }

        public static string GetDataSource(string reportFileName, string reportFolder)
        {
            string message = "";
            string reportPath = (reportFolder + "\\" + reportFileName + ".frx");

            FastReport.Report report1 = new FastReport.Report();

            try
            {
                report1.Load(reportPath);
            }
            catch
            {
                message = "File Not Found";
            }

            //var datasource = report1.GetDataSource("VoterDataConnection");
            message = report1.Dictionary.Connections[0].ConnectionString;

            //var connection = report1.Dictionary.Connections[0];

            //connection.GetConnection().ConnectionString = ConfigurationManager.ConnectionStrings["VoterDatabase"].ConnectionString;

            //foreach (FastReport.Data.MsSqlDataConnection connection in report1.Dictionary.Connections)
            //{
            //    message = connection.ConnectionString.ToString();
            //}

            return message;
        }

        public static string GetPrinterBins(PrinterSettingsModel printer)
        {
            string message = "";

            FastReport.Report report1 = new FastReport.Report();
            report1.PrintSettings.Printer = printer.ApplicationPrinter;
            report1.PrintSettings.PaperSource = printer.AppBin;

            message = report1.PrintSettings.Printer + " ";
            message += report1.PrintSettings.PaperSource + " ";

            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            foreach (System.Drawing.Printing.PaperSource item in printerSettings.PaperSources)
            {
                message += item.SourceName;
            }

            return message;
        }

    }
}
