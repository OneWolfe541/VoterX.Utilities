using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoterX.Core.Voters;
using VoterX.Utilities.Exceptions;
using VoterX.Utilities.Methods;
using VoterX.SystemSettings.Models;
using System.Diagnostics;
using System.IO;
using System.Threading;
using VoterX.Utilities.Dialogs;

namespace VoterX.Utilities.UserControls
{
    /// <summary>
    /// Interaction logic for SignatureControl.xaml
    /// </summary>
    public partial class SignatureUWPControl : UserControl
    {
        public event RoutedEventHandler EnablePadClickPreview;

        public event RoutedEventHandler EnablePadClick;

        public event RoutedEventHandler DeleteClick;

        public VoterDataModel Voter { get; set; }

        public string Folder { get; set; }

        public string Error { get; private set; }

        public SystemSettingsModel Settings { get; set; }

        public bool RefuseToSign
        {
            get
            {
                return !EnablePad.IsEnabled;
            }

            set
            {
                EnablePad.IsEnabled = !value;
            }
        }

        public bool IsSignatureLoaded
        {
            get
            {
                if(VoterSignature.Source == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            private set { IsSignatureLoaded = value; } 
        }

        private int UWP_Exit_Code = 0;

        public SignatureUWPControl()
        {
            InitializeComponent();
        }

        public bool LoadSignature()
        {
            if (Voter != null)
            {
                if (Folder != null)
                {
                    VoterSignature.Source = null;
                    VoterSignature.Source = SignatureMethods.LoadSignatureFromFile(Voter, Folder);

                    if (VoterSignature.Source != null)
                    {
                        Error = "Signature File Found";
                        EnablePad.IsEnabled = false;
                        return true;
                    }
                    else
                    {
                        Error = "Signature File Not Found";
                        return false;
                    }
                }
                else
                {
                    throw new CustomException("Folder Not Set!");
                }
            }
            else
            {
                throw new CustomException("Voter Not Set!");
            }
        }

        // Clear the image and delete the existing file
        public void DeleteExistingFile()
        {
            //LoggingMethods.LogIO("DELETE SIGNATURE IMAGE");

            // Clear the image control
            VoterSignature.Source = null;

            // Display which file is being deleted
            //StatusBar.ApplicationStatus("Searching For: " + voter.VoterID.ToString());

            if (SignatureMethods.DeleteVoterSignature(Voter, Folder))
            {
                //StatusBar.ApplicationStatus("File Deleted: " + voter.VoterID.ToString());
                Error = "File Deleted: " + Voter.VoterID.ToString();
            }
            else
            {
                //StatusBar.ApplicationStatus("File not found: " + voter.VoterID.ToString());
                Error = "File not found: " + Voter.VoterID.ToString();
            }
        }

        private void EnablePadButton_Click(object sender, RoutedEventArgs e)
        {
            EnablePad.IsEnabled = false;
            EnablePadClickPreview?.Invoke(sender, e);
            if (Voter != null)
            {
                if (Folder != null)
                {
                    if (Settings != null)
                    {
                        // Delete existing signature file
                        DeleteExistingFile();

                        //UWP_Exit_Code = await Task.Run(() => {
                        UWP_Exit_Code = OpenSignatureApp();

                        //AlertDialog messageDialog = new AlertDialog("Signature App Running: " + UWP_Exit_Code);
                        //messageDialog.ShowDialog();

                        //var picture = await Task.Run(() => CheckPicturesFolderAsync(Voter, Folder));
                        var picture = CheckPicturesFolderAsync(Voter, Folder);
                        if (picture == true)
                        {
                            VoterSignature.Source = SignatureMethods.LoadSignatureFromFile(Voter, Folder);

                            EnablePadClick?.Invoke(sender, e);
                        }
                        else
                        {
                            EnablePad.IsEnabled = true;
                        }
                    }
                    else
                    {
                        EnablePad.IsEnabled = true;
                        throw new CustomException("Settings Not Set!");
                    }
                }
                else
                {
                    EnablePad.IsEnabled = true;
                    throw new CustomException("Folder Not Set!");
                }
            }
            else
            {
                EnablePad.IsEnabled = true;
                throw new CustomException("Voter Not Set!");
            }            
        }

        private int OpenSignatureApp()
        {
            //Process P = Process.Start("C:\\Program Files\\AutoVote\\AutoVoteReconcile.exe");
            //P.WaitForExit();
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = @"signaturecapture:" + Voter.VoterID + "," + Voter.FirstName + "," + Voter.MiddleName + "," + Voter.LastName + "," + Voter.DOBYear;
            p.StartInfo = startInfo;

            try
            {
                p.Start();
                //p.WaitForExit();

                Process[] processlist = Process.GetProcesses();

                var list = processlist.ToList();

                return list.Where(l => l.ProcessName.Contains("Signature")).Count();
            }
            catch (Exception error)
            {
                var er = error.Message;
                AlertDialog messageDialog = new AlertDialog("Signature App Closed: " + er);
                messageDialog.ShowDialog();
                return 9; 
            }

            //return 0;
        }

        private bool CheckPicturesFolderAsync(VoterDataModel voter, string folder)
        {
            
            bool result = false;

            //try
            {
                string picturefolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                // Set the file path to voter's id
                string sourceFilePath = picturefolder + "\\AutoVote\\Signatures\\" + voter.VoterID.ToString() + ".jpg";

                string destFilePath = folder + "\\" + voter.VoterID.ToString() + ".jpg";

                bool controlBreak = false;
                while (!File.Exists(sourceFilePath) && controlBreak == false)
                {
                    //await PutTaskDelay(4000);
                    Thread.Sleep(2000);                   

                    if (GetSignatureProcess() < 1)
                    {
                        controlBreak = true;
                    }
                }

                // Check if the file actually exists before deleting
                if (File.Exists(sourceFilePath))
                {
                    // Move the File
                    File.Copy(sourceFilePath, destFilePath, true);

                    File.Delete(sourceFilePath);

                    // Display the Signature
                    //LoadSignature();
                    //VoterSignature.Source = null;
                    //VoterSignature.Source = SignatureMethods.LoadSignatureFromFile(voter, destFilePath);

                    result = true;
                }
            }
            //catch
            //{

            //}

            return result;
            
        }

        private int GetSignatureProcess()
        {
            Process[] processlist = Process.GetProcesses();
            var list = processlist.ToList();
            return list.Where(l => l.ProcessName.Contains("Signature")).Count();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Voter != null)
            {
                if (Folder != null)
                {
                    // Delete existing signature file
                    //DeleteExistingFile();                    

                    DeleteClick?.Invoke(sender, e);

                    EnablePad.IsEnabled = true;
                }
                else
                {
                    throw new CustomException("Folder Not Set!");
                }
            }
            else
            {
                throw new CustomException("Voter Not Set!");
            }
        }

        // https://stackoverflow.com/questions/22158278/wait-some-seconds-without-blocking-ui-execution
        private async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}
