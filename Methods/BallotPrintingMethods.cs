using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoVote.Logging;
using VoterX.Core.Voters;
using VoterX.SystemSettings;
using VoterX.SystemSettings.Enums;

namespace VoterX.Utilities.Methods
{
    public static class BallotPrinting
    {
        public static string PrintBallotBundle(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            // Get ballot file name from tblBallotstlesMaster
            // Get ballot folder from settings file
            string ballotPath = (settings.Ballots.BallotFolder + "\\" + voter.BallotStyleFile);

            //if (settings.Ballots.OlderSystem == false)
            {
                // Print Official Ballot
                message = PDFToolsMethods.PrintPDF(
                        settings.Printers.BallotPrinter,        // Printer Name
                        ballotPath,                             // Ballot PDF File
                        "Print Official Ballot",                // Job Name
                        settings.Printers.BallotBin,            // Ballot Paper Tray
                        (short)settings.Printers.BallotSize,    // Ballot Paper Size
                        1,                                      // PDF Page Number
                        settings.Ballots.Duplex,
                        false,
                        settings.System.PDFTools
                        );

                AutoVoteLogger _reportLogger = new AutoVoteLogger("BallotLogs", settings.System.ReportErrorLogging);
                _reportLogger.WriteLog("Print Official Ballot: " + ballotPath + " :: " + message);

            }
            //else
            //{
            //    message = ReportPrintingMethods.PrintTrainingBallot(voter, settings);
            //}

            // Date based function
            //if (AppSettings.Election.IsElectionDay())
            // System Mode based function
            if (settings.System.VCCType == VotingCenterMode.ElectionDay)
            {
                if (settings.System.Permit == 1)
                {
                    // If Election day mode && Permit is on
                    // Print Spoiled Permit
                    message = ReportPrintingMethods.PrintVoterPermit(voter, settings);
                }
            }
            else
            {
                // If Early voting mode
                // Print Application
                message = ReportPrintingMethods.PrintVoterApplication(voter, settings);
            }

            if (settings.System.BallotStub == 1)
            {
                // If stub is turned on
                // Print Ballot Stub
                message = ReportPrintingMethods.PrintBallotStub(voter, settings);
            }

            return message;
        }

        public static string ReprintBallot(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            string ballotPath = settings.Ballots.BallotFolder + "\\" + voter.BallotStyleFile;

            // Print Official Ballot
            message = PDFToolsMethods.PrintPDF(
                    settings.Printers.BallotPrinter,        // Printer Name
                    ballotPath,                             // Ballot PDF File
                    "Print Official Ballot",                // Job Name
                    settings.Printers.BallotBin,            // Ballot Paper Tray
                    (short)settings.Printers.BallotSize,    // Ballot Paper Size
                    1,                                      // PDF Page Number
                    settings.Ballots.Duplex,
                    false,
                    settings.System.PDFTools
                    );

            // DO NOT REPRINT PERMIT
            //if (AppSettings.Election.IsElectionDay())
            //if (settings.System.VCCType == VotingCenterMode.ElectionDay)
            //{
            //    if (settings.System.Permit == 1)
            //    {
            //        string permitPath = settings.Reports.ReportsFolder + "\\UNFinalPDF.pdf";

            //        // If Election day mode
            //        // Print Spoiled Permit
            //        PDFToolsMethods.PrintPDF(
            //                settings.Printers.ApplicationPrinter,       // Printer Name
            //                permitPath,                                 // Report PDF File
            //                "Print Voting Permit",                      // Job Name
            //                settings.Printers.AppBin,                   // Report Paper Tray
            //                (short)settings.Printers.AppSize,           // Report Paper Size
            //                null,                                       // PDF Page Number
            //                settings.Ballots.Duplex,
            //                false,
            //                settings.System.PDFTools
            //                );
            //    }
            //}

            return message;
        }

        public static string ReprintApplication(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintVoterApplication(voter, settings);

            return message;
        }

        public static string ReprintPermit(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintVoterPermit(voter, settings);

            return message;
        }

        public static string ReprintPermitSpoiled(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintVoterPermitSpoiled(voter, settings);

            return message;
        }

        public static string ReprintStub(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintBallotStub(voter, settings);

            return message;
        }

        public static string PrintProvisionalBallot(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            string provisionalPath = settings.Ballots.ProvisionalFolder + "\\" + settings.Ballots.ProvisionalPrefix + voter.BallotStyleFile;

            // Print Unofficial Ballot
            message = PDFToolsMethods.PrintPDF(
                    settings.Printers.BallotPrinter,        // Printer Name
                    provisionalPath,                        // Ballot PDF File
                    "Print Provisional Ballot",             // Job Name
                    settings.Printers.BallotBin,            // Ballot Paper Tray
                    (short)settings.Printers.BallotSize,    // Ballot Paper Size
                    1,                                      // PDF Page Number
                    settings.Ballots.Duplex,
                    false,
                    settings.System.PDFTools
                    );

            return message;
        }

        public static string PrintTestBallot(SystemSettingsController settings)
        {
            string message = "";

            string testPath = settings.Ballots.TestBallot;

            // Print Unofficial Ballot
            message = PDFToolsMethods.PrintPDF(
                    settings.Printers.BallotPrinter,        // Printer Name
                    testPath,                               // Ballot PDF File
                    "Print Test Ballot",                    // Job Name
                    settings.Printers.BallotBin,            // Ballot Paper Tray
                    (short)settings.Printers.BallotSize,    // Ballot Paper Size
                    1,                                      // PDF Page Number
                    settings.Ballots.Duplex,
                    false,
                    settings.System.PDFTools
                    );

            return message;
        }

        public static string PrintSignatureForm(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintSignatureForm(voter, settings);

            return message;
        }

        public static string PrintEmergencyBallot(SystemSettingsController settings, string ballotstylefile)
        {
            string message = "";

            string ballotPath = settings.Ballots.BallotFolder + "\\" + ballotstylefile;

            // Print Unofficial Ballot
            message = PDFToolsMethods.PrintPDF(
                    settings.Printers.BallotPrinter,            // Printer Name
                    ballotPath,                                 // Ballot PDF File
                    "Print Test Ballot",                        // Job Name
                    settings.Printers.BallotBin,                // Ballot Paper Tray
                    (short)settings.Printers.BallotSize,        // Ballot Paper Size
                    1,                                          // PDF Page Number
                    settings.Ballots.Duplex,
                    false,
                    settings.System.PDFTools
                    );

            return message;
        }

        public static string PrintSampleBallot(SystemSettingsController settings, string ballotstylefile)
        {
            string message = "";

            string ballotPath = settings.Ballots.SampleFolder + "\\SAM_" + ballotstylefile;

            // Print Unofficial Ballot
            message = PDFToolsMethods.PrintPDF(
                    settings.Printers.SamplePrinter,            // Printer Name
                    ballotPath,                                 // Ballot PDF File
                    "Print Test Ballot",                        // Job Name
                    settings.Printers.SampleBin,                // Ballot Paper Tray
                    (short)settings.Printers.SampleSize,        // Ballot Paper Size
                    1,                                          // PDF Page Number
                    settings.Ballots.Duplex,
                    false,
                    settings.System.PDFTools
                    );

            return message;
        }

        //public static string PrintSampleCopies(SystemSettingsController settings, string BallotStyleFile, int copies)
        //{
        //    string message = "";

        //    string ballotPath = (settings.Ballots.SampleFolder + "\\" + BallotStyleFile);

        //    // Print Official Ballot
        //    message = PDFToolsMethods.PrintPDF(
        //            settings.Printers.BallotPrinter,         // Printer Name
        //            ballotPath,                                 // Ballot PDF File
        //            "Print Emergency Ballot ",                  // Job Name
        //            settings.Printers.BallotBin,             // Ballot Paper Tray
        //            (short)settings.Printers.BallotSize,     // Ballot Paper Size
        //            1,                                          // PDF Page Number
        //            (short)copies
        //            );

        //    return message;
        //}

        public static string PrintAffidavit(VoterDataModel voter, SystemSettingsController settings)
        {
            string message = "";

            message = ReportPrintingMethods.PrintAbsenteeAffidavit(voter, settings);

            return message;
        }
    }
}
