using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.Extensions;
using VoterX.SystemSettings;
using VoterX.Core.Voters;

namespace VoterX.Utilities.Methods
{
    public static class VoterManagmentMethods
    {
        /// <summary>
        /// Mark Voter as Voted at Polls and Returns any Error Messages
        /// </summary>
        /// <param name = "voter" ></ param >
        /// < returns ></ returns >
        //public static string InsertVotedAtPolls(VoterDataModel voter, VoterContainer container, SystemSettingsController settings)
        //{
        //    string message = "";

        //    using (VoterDatabase _VoterDB = container.VoterDatabase())
        //    {
        //        check if voted Record exists
        //        if (_VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID)) == null)
        //        {
        //            Create voted record template
        //            avVotedRecord newVotedRecord = new avVotedRecord();
        //            Map fields to template
        //            newVotedRecord.voter_id = Int32.Parse(voter.VoterID);
        //            newVotedRecord.county_code = voter.County;
        //            newVotedRecord.election_id = settings.Election.ElectionID;
        //            newVotedRecord.date_voted = DateTime.Now;
        //            newVotedRecord.printed_date = DateTime.Now;
        //            newVotedRecord.poll_id = settings.System.SiteID;
        //            newVotedRecord.computer = settings.System.MachineID;
        //            newVotedRecord.ballot_style_id = voter.BallotStyleID;
        //            newVotedRecord.precinct_part_id = voter.PrecinctPartID;
        //            newVotedRecord.sign_refused = voter.SignRefused;
        //            newVotedRecord.user_id = settings.User.UserID;
        //            newVotedRecord.log_code = settings.System.VCCType == 2 ? 16 : 8;
        //            newVotedRecord.activity_code = settings.System.VCCType == 2 ? "P" : "A";
        //            newVotedRecord.activity_date = DateTime.Now;
        //            newVotedRecord.local_only = true;
        //            newVotedRecord.fled_voter = false;
        //            newVotedRecord.wrong_voter = false;
        //            newVotedRecord.last_synced = DateTime.Parse("1/1/1");

        //            Add voters address
        //            newVotedRecord.address_line_1 = voter.Address1;
        //            newVotedRecord.address_line_2 = voter.Address2;
        //            newVotedRecord.address_city = voter.City;
        //            newVotedRecord.address_state = voter.State;
        //            newVotedRecord.address_zip = voter.Zip;
        //            newVotedRecord.address_country = voter.Country;

        //            On election day voters will be assigned a ballot number
        //            if (voter.BallotNumber != null)
        //            {
        //                newVotedRecord.ballot_number = voter.BallotNumber;
        //            }

        //            try
        //            {
        //                Insert new voted record from template
        //                _VoterDB.avVotedRecords.Add(newVotedRecord);
        //                _VoterDB.SaveChanges();
        //                message = "Voter Saved";
        //            }
        //            catch (Exception e)
        //            {
        //                message = e.InnerException.ToString();
        //            }

        //        } // end if voter exists

        //    } // end using _VoterDB

        //    return message;
        //}

        /// <summary>
        /// Mark Voter as Voted at Polls and Returns any Error Messages
        /// </summary>
        /// <param name="voter"></param>
        /// <returns></returns>
        // Change added 8/7/2018 because InsertVotedAtPolls() would fail to update existing absentee records
        public static string InsertOrUpdateVotedAtPolls(NMVoter voter, SystemSettingsController settings)
        {
            string message = "";

            // Set voter's ballot number to the highest value at the given site
            voter.GetNextBallotNumber((int)settings.System.SiteID);

            // Set local values
            voter.Data.ElectionID = settings.Election.ElectionID;
            voter.Data.PollID = settings.System.SiteID;
            voter.Data.ComputerID = settings.System.MachineID;
            voter.Data.UserId = settings.User.UserID;

            //var test = voter.ConvertToActivity();

            // Save as Voted at Polls
            try
            {
                voter.VotedAtPolls((int)settings.System.VCCType);
            }
            catch (Exception e)
            {
                message = e.Message;
                if (e.InnerException != null)
                {
                    message += e.InnerException.ToString();

                    if (e.InnerException.InnerException != null)
                    {
                        message += e.InnerException.InnerException.ToString();
                    }
                }
            }

            //using (VoterDatabase _VoterDB = container.VoterDatabase())
            //{
            //    // check if voted Record exists
            //    var votedRecord = _VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID));
            //    if (votedRecord == null)
            //    {
            //        // Create voted record template
            //        avVotedRecord newVotedRecord = new avVotedRecord();
            //        // Map fields to template
            //        newVotedRecord.voter_id = Int32.Parse(voter.VoterID);
            //        newVotedRecord.county_code = voter.County;
            //        newVotedRecord.election_id = settings.Election.ElectionID;
            //        newVotedRecord.date_voted = DateTime.Now;
            //        newVotedRecord.printed_date = DateTime.Now;
            //        newVotedRecord.poll_id = settings.System.SiteID;
            //        newVotedRecord.computer = settings.System.MachineID;
            //        newVotedRecord.ballot_style_id = voter.BallotStyleID;
            //        newVotedRecord.precinct_part_id = voter.PrecinctPartID;
            //        newVotedRecord.sign_refused = voter.SignRefused;
            //        newVotedRecord.user_id = settings.User.UserID;
            //        newVotedRecord.log_code = settings.System.VCCType == 2 ? 16 : 8;
            //        newVotedRecord.activity_code = settings.System.VCCType == 2 ? "P" : "E";
            //        newVotedRecord.activity_date = DateTime.Now;
            //        newVotedRecord.local_only = true;
            //        newVotedRecord.fled_voter = false;
            //        newVotedRecord.wrong_voter = false;
            //        newVotedRecord.last_synced = DateTime.Parse("1/1/1");

            //        // Add voters address
            //        newVotedRecord.address_line_1 = voter.Address1;
            //        newVotedRecord.address_line_2 = voter.Address2;
            //        newVotedRecord.address_city = voter.City;
            //        newVotedRecord.address_state = voter.State;
            //        newVotedRecord.address_zip = voter.Zip;
            //        newVotedRecord.address_country = voter.Country;

            //        // On election day voters will be assigned a ballot number
            //        if (voter.BallotNumber != null)
            //        {
            //            newVotedRecord.ballot_number = voter.BallotNumber;
            //        }

            //        // Add record to upload queue
            //        avVotedRecordsSync newVoterSync = new avVotedRecordsSync();
            //        newVoterSync.voter_sync_id = Guid.NewGuid();
            //        newVoterSync.voter_id = Int32.Parse(voter.VoterID);
            //        newVoterSync.QueueTime = DateTime.Now;

            //        _VoterDB.avVotedRecordsSyncs.Add(newVoterSync);

            //        try
            //        {
            //            // Insert new voted record from template
            //            _VoterDB.avVotedRecords.Add(newVotedRecord);
            //            _VoterDB.SaveChanges();
            //            message = "Voter Saved";
            //        }
            //        catch (Exception e)
            //        {
            //            message = e.InnerException.ToString();
            //        }

            //    }
            //    else
            //    {
            //        // Check if voter has been assigned a ballot
            //        if (votedRecord.log_code < 5)
            //        {
            //            // Update voted record
            //            votedRecord.date_voted = DateTime.Now;
            //            votedRecord.printed_date = DateTime.Now;
            //            votedRecord.poll_id = settings.System.SiteID;
            //            votedRecord.computer = settings.System.MachineID;
            //            votedRecord.ballot_style_id = voter.BallotStyleID;
            //            votedRecord.precinct_part_id = voter.PrecinctPartID;
            //            votedRecord.sign_refused = voter.SignRefused;
            //            votedRecord.user_id = settings.User.UserID;
            //            votedRecord.log_code = settings.System.VCCType == 2 ? 16 : 8;
            //            votedRecord.activity_code = settings.System.VCCType == 2 ? "P" : "E";
            //            votedRecord.activity_date = DateTime.Now;
            //            votedRecord.local_only = true;
            //            votedRecord.fled_voter = false;
            //            votedRecord.wrong_voter = false;
            //            votedRecord.last_synced = DateTime.Parse("1/1/1");

            //            // On election day voters will be assigned a ballot number
            //            if (voter.BallotNumber != null)
            //            {
            //                votedRecord.ballot_number = voter.BallotNumber;
            //            }

            //            // Add record to upload queue
            //            avVotedRecordsSync newVoterSync = new avVotedRecordsSync();
            //            newVoterSync.voter_sync_id = Guid.NewGuid();
            //            newVoterSync.voter_id = Int32.Parse(voter.VoterID);
            //            newVoterSync.QueueTime = DateTime.Now;

            //            _VoterDB.avVotedRecordsSyncs.Add(newVoterSync);

            //            try
            //            {
            //                // Insert new voted record from template
            //                _VoterDB.SaveChanges();
            //                message = "Voter Saved";
            //            }
            //            catch (Exception e)
            //            {
            //                message = e.InnerException.ToString();
            //            }
            //        }
            //        else
            //        {
            //            message = "Voter has already been assigned a ballot";
            //        }
            //    } // end if voter exists

            //} // end using _VoterDB

            return message;
        }

        public static string InsertOrUpdateVotedAtPolls(NMVoter voter, SystemSettingsController settings, bool Voided)
        {
            string message = "";

            // Set voter's ballot number to the highest value at the given site
            voter.GetNextBallotNumber((int)settings.System.SiteID);

            // Set local values
            voter.Data.ElectionID = settings.Election.ElectionID;
            voter.Data.PollID = settings.System.SiteID;
            voter.Data.ComputerID = settings.System.MachineID;
            voter.Data.UserId = settings.User.UserID;

            // Save as Voted at Polls
            try
            {
                voter.VotedAtPolls((int)settings.System.VCCType, Voided);
            }
            catch (Exception e)
            {
                message = e.Message;
                if (e.InnerException != null)
                {
                    message += e.InnerException.ToString();
                }
            }

            return message;
        }

        public static string InsertSpoiledBallot(NMVoter voter, int spoiledReasonId, SystemSettingsController settings)
        {
            string message = "";

            // Set local values
            voter.Data.ElectionID = settings.Election.ElectionID;
            voter.Data.PollID = settings.System.SiteID;
            voter.Data.ComputerID = settings.System.MachineID;
            voter.Data.UserId = settings.User.UserID;

            // Save create new Spoiled Record
            try
            {
                voter.SpoilBallot(spoiledReasonId);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    message = e.InnerException.ToString();
                }
                else
                {
                    message = e.Message;
                }
            }

            //using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
            //{
            //    // Create voted record template
            //    avSpoiled newSpoiledBallot = new avSpoiled();

            //    string timestamp = settings.System.MachineID.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //    newSpoiledBallot.spoiled_ballot_id = Int32.Parse(timestamp);

            //    // Map fields to template
            //    newSpoiledBallot.voter_id = Int32.Parse(voter.VoterID);
            //    newSpoiledBallot.printed_date = DateTime.Now;
            //    //newSpoiledBallot.ballot_number = voter.BallotStyleID;
            //    newSpoiledBallot.spoiled_reason = spoiledReasonId;
            //    newSpoiledBallot.poll_id = (int)settings.System.SiteID;
            //    newSpoiledBallot.computer = settings.System.MachineID;
            //    newSpoiledBallot.user_id = settings.User.UserID;
            //    newSpoiledBallot.ballot_surrendered = voter.BallotSurrendered;
            //    newSpoiledBallot.local_only = true;

            //    // On election day voters will be assigned a ballot number
            //    if (voter.BallotNumber != null)
            //    {
            //        newSpoiledBallot.ballot_number = voter.BallotNumber;
            //    }

            //    try
            //    {
            //        //Insert new spoiled record from template
            //        _VoterDB.avSpoileds.Add(newSpoiledBallot);
            //        _VoterDB.SaveChanges();
            //        result = true;
            //    }
            //    catch
            //    {
            //        _VoterDB.Dispose();
            //    }
            //}

            return message;
        }

        public static string UpdateVoterBallotNumber(NMVoter voter, SystemSettingsController settings)
        {
            string message = "";

            // Set voter's ballot number to the highest value at the given site
            voter.GetNextBallotNumber((int)settings.System.SiteID);

            // Save create new Spoiled Record
            try
            {
                voter.UpdateBallotNumber();
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            //using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
            //{
            //    avVotedRecord voterRecord = _VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID));
            //    // check if voted Record exists
            //    if (voter != null)
            //    {
            //        voterRecord.ballot_number = voter.BallotNumber;
            //        // New ballot numbers should not be synced with the central server 7/24/2018
            //        //voterRecord.local_only = true;

            //        _VoterDB.SaveChanges();
            //    }
            //}

            return message;
        }

        public static string MarkAsWrongVoter(NMVoter voter)
        {
            string message = "";

            try
            {
                voter.UpdateWrongVoter();
            }
            catch (Exception e)
            {
                message = e.InnerException.ToString();
            }

            //using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
            //{
            //    avVotedRecord wrongVoter = _VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID));
            //    // check if voted Record exists
            //    if (wrongVoter != null)
            //    {
            //        //wrongVoter.log_code = 1;
            //        wrongVoter.wrong_voter = true;
            //        wrongVoter.activity_date = DateTime.Now;
            //        wrongVoter.local_only = true;

            //        _VoterDB.SaveChanges();
            //    }
            //}

            return message;
        }

        public static string MarkAsFledVoter(NMVoter voter)
        {
            string message = "";

            try
            {
                voter.UpdateFledVoter();
            }
            catch (Exception e)
            {
                message = e.InnerException.ToString();
            }

            //using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
            //{
            //    avVotedRecord fledVoter = _VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID));
            //    // check if voted Record exists
            //    if (fledVoter != null)
            //    {
            //        //wrongVoter.log_code = 1;
            //        fledVoter.fled_voter = true;
            //        fledVoter.activity_date = DateTime.Now;
            //        fledVoter.local_only = true;

            //        _VoterDB.SaveChanges();
            //    }
            //}

            return message;
        }

        public static string MarkAsNotTabulated(NMVoter voter)
        {
            string message = "";

            try
            {
                voter.UpdateNotTabulated();
            }
            catch (Exception e)
            {
                message = e.InnerException.ToString();
            }

            return message;
        }

        public static string InsertProvisionalBallot(NMVoter voter, int provisionalReasonId, SystemSettingsController settings)
        {
            //LoggingMethods.LogObject("INSERTPROVISIONALBALLOT CALLED");

            string message = "";

            // Set local values
            voter.Data.ElectionID = settings.Election.ElectionID;
            voter.Data.PollID = settings.System.SiteID;
            voter.Data.ComputerID = settings.System.MachineID;
            voter.Data.UserId = settings.User.UserID;

            // Save create new Spoiled Record
            try
            {
                voter.ProvisionalBallot(provisionalReasonId);
            }
            catch (Exception e)
            {
                message = e.InnerException.ToString();
            }

            //using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
            //{
            //    // Create voted record template
            //    avProvisional newProvisionalVoter = new avProvisional();

            //    string timestamp = settings.System.MachineID.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //    newProvisionalVoter.provisional_ballot_id = Int32.Parse(timestamp);

            //    // Map fields to template
            //    newProvisionalVoter.voter_id = Int32.Parse(voter.VoterID);
            //    ////newProvisionalVoter.name_title = voter.Title;
            //    newProvisionalVoter.name_first = voter.FirstName;
            //    newProvisionalVoter.name_middle = voter.MiddleName;
            //    newProvisionalVoter.name_last = voter.LastName;
            //    newProvisionalVoter.dob = voter.DOBYear;
            //    // Get address info from tblStreet
            //    //newProvisionalVoter.house_number = voter.HouseNumber;
            //    //newProvisionalVoter.street_prefix = voter.StreetPrefix;
            //    //newProvisionalVoter.street_name = voter.StreetName;
            //    //newProvisionalVoter.street_type = voter.StreetType;
            //    //newProvisionalVoter.street_suffix = voter.StreetSuffix;
            //    //newProvisionalVoter.unit = voter.Unit;
            //    //newProvisionalVoter.unit_number = voter.UnitNumber;
            //    //newProvisionalVoter.city = voter.City;
            //    //newProvisionalVoter.state = voter.State;
            //    //newProvisionalVoter.zip = voter.Zip;
            //    newProvisionalVoter.address = voter.Address1;
            //    newProvisionalVoter.address_2 = voter.Address2;
            //    newProvisionalVoter.city = voter.City;
            //    newProvisionalVoter.state = voter.State;
            //    newProvisionalVoter.zip = voter.Zip;
            //    newProvisionalVoter.ballot_style_id = voter.BallotStyleID;
            //    newProvisionalVoter.printed_date = DateTime.Now;
            //    newProvisionalVoter.provisional_reason = provisionalReasonId;
            //    newProvisionalVoter.poll_id = (int)settings.System.SiteID;
            //    newProvisionalVoter.computer = settings.System.MachineID;
            //    newProvisionalVoter.user_id = settings.User.UserID;
            //    newProvisionalVoter.last_modified = DateTime.Now;
            //    newProvisionalVoter.local_only = true;

            //    try
            //    {
            //        // Insert new provisional record from template
            //        _VoterDB.avProvisionals.Add(newProvisionalVoter);
            //        _VoterDB.SaveChanges();
            //        result = true;

            //        //LoggingMethods.LogIO("PROVISIONAL BALLOT SAVED FOR VOTER: " + newProvisionalVoter.voter_id.ToString());
            //    }
            //    catch (Exception e)
            //    {
            //        e.Message.ToString();
            //        _VoterDB.Dispose();
            //    }
            //}

            return message;
        }

        //public static bool InsertQuarantine(VoterDataModel voter, string quarantineMessage, VoterContainer container, SystemSettingsController settings)
        //{
        //    bool result = false;

        //    using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
        //    {
        //        // Create voted record template
        //        syncQuarantined newQuarantineRecord = new syncQuarantined();
        //        // Map fields to template
        //        newQuarantineRecord.voter_id = Int32.Parse(voter.VoterID);
        //        newQuarantineRecord.county_code = voter.County;
        //        newQuarantineRecord.election_id = settings.Election.ElectionID;
        //        newQuarantineRecord.date_voted = DateTime.Now;
        //        newQuarantineRecord.poll_id = settings.System.SiteID;
        //        newQuarantineRecord.computer = settings.System.MachineID;
        //        newQuarantineRecord.ballot_style_id = voter.BallotStyleID;
        //        newQuarantineRecord.precinct_part_id = voter.PrecinctPartID;
        //        newQuarantineRecord.sign_refused = voter.SignRefused;
        //        newQuarantineRecord.user_id = settings.User.UserID;
        //        newQuarantineRecord.log_code = settings.System.VCCType == 2 ? 16 : 8;
        //        newQuarantineRecord.activity_code = settings.System.VCCType == 2 ? "P" : "A";
        //        newQuarantineRecord.activity_date = DateTime.Now;
        //        newQuarantineRecord.local_only = true;
        //        newQuarantineRecord.fled_voter = false;
        //        newQuarantineRecord.wrong_voter = false;
        //        newQuarantineRecord.last_synced = DateTime.Parse("1/1/1");

        //        // Add voters address
        //        newQuarantineRecord.address_line_1 = voter.Address1;
        //        newQuarantineRecord.address_line_2 = voter.Address2;
        //        newQuarantineRecord.address_city = voter.City;
        //        newQuarantineRecord.address_state = voter.State;
        //        newQuarantineRecord.address_zip = voter.Zip;
        //        newQuarantineRecord.address_country = voter.Country;

        //        // Quarantine Message
        //        newQuarantineRecord.quarantined_message = quarantineMessage;

        //        try
        //        {
        //            // Insert new voted record from template
        //            _VoterDB.syncQuarantineds.Add(newQuarantineRecord);
        //            _VoterDB.SaveChanges();
        //            result = true;
        //        }
        //        catch
        //        {
        //            _VoterDB.Dispose();
        //        }

        //    } // end using _VoterDB

        //    return result;
        //}

        //public static bool EligibleToVoteAtSite(VoterDataModel voter, VoterContainer container, SystemSettingsController settings)
        //{
        //    bool result = false;
        //    using (VoterDatabase _VoterDB = container.Resolve<VoterDatabase>())
        //    {
        //        var pollsite = _VoterDB.tblElectionPrecinctPolls
        //            .Where(site =>
        //                site.precinct_part_id == voter.PrecinctPartID &&
        //                site.poll_id == settings.System.SiteID);
        //        if (pollsite != null) result = true;
        //    }
        //    return result;
        //}

        //// Post check logic added 8/7/2018 in order to check if any changes have been made to the log code before a ballot is printed
        //public static int? CheckLogCode(VoterDataModel voter, VoterContainer container)
        //{
        //    using (VoterDatabase _VoterDB = container.VoterDatabase())
        //    {
        //        // check if voted Record exists
        //        try
        //        {
        //            var logCode = _VoterDB.avVotedRecords.Find(Int32.Parse(voter.VoterID)).log_code;
        //            if (logCode != null) return logCode;
        //            else return 0;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //}
    }
}
