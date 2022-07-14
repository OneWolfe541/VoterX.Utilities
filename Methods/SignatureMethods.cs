using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using eSign3;
using VoterX.Core.Voters;
using VoterX.Utilities.Adapters;
using VoterX.Core.Extensions;
using AutoVote.Logging;
using VoterX.Utilities.Extensions;

namespace VoterX.Utilities.Methods
{
    public static class SignatureMethods
    {
        private static eSign3.esCapture epad = new esCapture();

        public static BitmapImage LoadSignatureFromFile(VoterDataModel voter, string folder)
        {
            var bitmap = new BitmapImage();

            // Get file name from Voter ID
            string path = folder + "\\" + voter.VoterID.ToString() + ".jpg";

            if (Windows10.IsWindows10())
            {
                bitmap = new BitmapImage(new Uri(path));
            }
            else
            {
                try
                {
                    // File stream loading avoids some of the caching issues inheirent with more direct methods
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        // The BeginInit and CacheOption prevents the local file from being locked
                        // which in turn allows the file to be overwritten or deleted while still loaded on the page
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                    }
                }
                catch (Exception e)
                {
                    bitmap = null;
                    throw (e);
                }
            }

            return bitmap;
        }

        public static bool VoterSignatureExists(VoterDataModel voter, string folder)
        {
            bool result = false;

            try
            {
                // Set the file path to voter's id
                string filePath = folder + "\\" + voter.VoterID.ToString() + ".jpg";

                // Check if the file actually exists before deleting
                if (File.Exists(filePath))
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                var test = e.Message;
            }

            return result;
        }

        public static bool DeleteVoterSignature(VoterDataModel voter, string folder)
        {
            bool result = false;

            try
            {

                // Set the file path to voter's id
                string filePath = folder + "\\" + voter.VoterID.ToString() + ".jpg";

                // Check if the file actually exists before deleting
                if (File.Exists(filePath))
                {
                    // Delete the file
                    File.Delete(filePath);

                    result = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        // Turn on the signature pad and wait for voter to sign or cancel
        public static bool SaveSignatureFromPad(VoterDataModel voter, string folder, int location, string siteName, string affText, string userText)
        {
            bool result = false;

            // Set affirmation and voter strings
            //string affText = "I, " + _voter.FirstName + " " + _voter.LastName + "confirm that I am a Regsitered Voter and to my knowledge have not cast a ballot in this election.";
            //string userText = _voter.FirstName + " " + _voter.LastName + " Party: " + _voter.Party + " Birth Date: " + _voter.DOBSearch;            

            // Set display settings
            epad.SetSignAppearanceOptionsEx(0, 1, 1, 1, 0, 0);

            // Set designer setails
            epad.SetSignerDetailsEx(voter.FirstName + " " + voter.LastName + " " + location.ToString(), null, null, null, null, null, null, null);

            // Set affirmation text on sig pad
            epad.SetAffirmationText(affText);

            // Set voter details on sig pad
            epad.SetUserInfo(userText);

            // Open connection and start the signature process
            // When OK is pressed on the sig pad save and reload the image file
            var epadButtonPress = epad.StartSign();
            if (epadButtonPress == ButtonPressed.BT_PRESSED_OK)
            {
                try
                {
                    // Save signature to jpg file
                    epad.SaveToFile(folder + "\\" + voter.VoterID.ToString() + ".jpg", 180, 110, FILETYPE.JPEG);

                    // Set meta tags on jpg
                    var jpeg = new JpegMetadataAdapter(folder + "\\" + voter.VoterID.ToString() + ".jpg");
                    //string voterName = voter.LastName + " " + voter.Generation + ", " + voter.FirstName + " " + voter.MiddleName;
                    if (!voter.FirstName.IsNullOrEmpty()) jpeg.Metadata.Copyright = voter.FirstName;
                    if (!voter.LastName.IsNullOrEmpty()) jpeg.Metadata.Comment = voter.LastName;
                    if (!voter.MiddleName.IsNullOrEmpty()) jpeg.Metadata.CameraModel = voter.MiddleName;
                    if (!voter.Generation.IsNullOrEmpty()) jpeg.Metadata.CameraManufacturer = voter.Generation;
                    if (!location.ToString().IsNullOrEmpty()) jpeg.Metadata.Title = location.ToString();
                    if (!siteName.IsNullOrEmpty()) jpeg.Metadata.Subject = siteName;
                    jpeg.Metadata.DateTaken = DateTime.Now.ToString();

                    if (!voter.ElectionID.ToString().IsNullOrEmpty() && !voter.County.IsNullOrEmpty())
                    {
                        List<string> authors = new List<string>();
                        authors.Add(voter.ElectionID.ToString());
                        authors.Add(voter.County);
                        var authorsList = new System.Collections.ObjectModel.ReadOnlyCollection<string>(authors);
                        jpeg.Metadata.Author = authorsList;
                    }

                    jpeg.Save();              // Saves the jpeg in-place

                    result = true;
                }
                catch (Exception e)
                {
                    // IF CORRUPTED ERRORS PERSIST YOU MAY HAVE TO TRY ONE OF THESE METHODS INSTEAD
                    // https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2153-avoid-handling-corrupted-state-exceptions?view=vs-2019

                    AutoVoteLogger signatureLog = new AutoVoteLogger("VCClogs", true);
                    signatureLog.WriteLog("Error saving signature file: Voter " + voter.VoterID.ToString() + " : ", e);
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool SaveSignatureFromPad(string fileName, string folder, int location, string siteName, string affText, string userText)
        {
            bool result = false;

            // Set affirmation and voter strings
            //string affText = "I, " + _voter.FirstName + " " + _voter.LastName + "confirm that I am a Regsitered Voter and to my knowledge have not cast a ballot in this election.";
            //string userText = _voter.FirstName + " " + _voter.LastName + " Party: " + _voter.Party + " Birth Date: " + _voter.DOBSearch;            

            // Set display settings
            epad.SetSignAppearanceOptionsEx(0, 1, 1, 1, 0, 0);

            // Set designer setails
            epad.SetSignerDetailsEx("Site" + " " + "Tester" + " " + location.ToString(), null, null, null, null, null, null, null);

            // Set affirmation text on sig pad
            epad.SetAffirmationText(affText);

            // Set voter details on sig pad
            epad.SetUserInfo(userText);

            // Open connection and start the signature process
            // When OK is pressed on the sig pad save and reload the image file
            var epadButtonPress = epad.StartSign();
            if (epadButtonPress == ButtonPressed.BT_PRESSED_OK)
            {
                try
                {
                    // Save signature to jpg file
                    epad.SaveToFile(folder + "\\" + fileName + ".jpg", 180, 110, FILETYPE.JPEG);

                    //// Set meta tags on jpg
                    //var jpeg = new JpegMetadataAdapter(folder + "\\" + voter.VoterID.ToString() + ".jpg");
                    ////string voterName = voter.LastName + " " + voter.Generation + ", " + voter.FirstName + " " + voter.MiddleName;
                    //if (!voter.FirstName.IsNullOrEmpty()) jpeg.Metadata.Copyright = voter.FirstName;
                    //if (!voter.LastName.IsNullOrEmpty()) jpeg.Metadata.Comment = voter.LastName;
                    //if (!voter.MiddleName.IsNullOrEmpty()) jpeg.Metadata.CameraModel = voter.MiddleName;
                    //if (!voter.Generation.IsNullOrEmpty()) jpeg.Metadata.CameraManufacturer = voter.Generation;
                    //if (!location.ToString().IsNullOrEmpty()) jpeg.Metadata.Title = location.ToString();
                    //if (!siteName.IsNullOrEmpty()) jpeg.Metadata.Subject = siteName;
                    //jpeg.Metadata.DateTaken = DateTime.Now.ToString();

                    //if (!voter.ElectionID.ToString().IsNullOrEmpty() && !voter.County.IsNullOrEmpty())
                    //{
                    //    List<string> authors = new List<string>();
                    //    authors.Add(voter.ElectionID.ToString());
                    //    authors.Add(voter.County);
                    //    var authorsList = new System.Collections.ObjectModel.ReadOnlyCollection<string>(authors);
                    //    jpeg.Metadata.Author = authorsList;
                    //}

                    //jpeg.Save();              // Saves the jpeg in-place

                    result = true;
                }
                catch (Exception e)
                {
                    // IF CORRUPTED ERRORS PERSIST YOU MAY HAVE TO TRY ONE OF THESE METHODS INSTEAD
                    // https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2153-avoid-handling-corrupted-state-exceptions?view=vs-2019

                    AutoVoteLogger signatureLog = new AutoVoteLogger("VCClogs", true);
                    signatureLog.WriteLog("Error saving signature file: Voter " + fileName + " : ", e);
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static string CheckSignaturePad()
        {
            //return epad.ConnectedDevice;
            return "";
        }

        public static async Task<string> CheckSignaturePadAsync()
        {
            return await Task.Run(() =>
            {
                var test = epad.ConnectedDevice;

                return test;
            });
            //return await Task.Run(() => "");
        }
    }
}
