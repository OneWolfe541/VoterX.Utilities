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

namespace VoterX.Utilities.UserControls
{
    /// <summary>
    /// Interaction logic for SignatureControl.xaml
    /// </summary>
    public partial class SignatureControl : UserControl
    {
        public event RoutedEventHandler EnablePadClickPreview;

        public event RoutedEventHandler EnablePadClick;

        public event RoutedEventHandler DeleteClick;

        public VoterDataModel Voter { get; set; }

        public string Folder { get; set; }

        public string Error { get; private set; }

        public SystemSettingsModel Settings { get; set; }

        public bool CanEnablePad
        {
            get
            {
                return EnablePad.IsEnabled;
            }

            set
            {
                EnablePad.IsEnabled = value;
            }
        }

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

        public SignatureControl()
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

        private async void EnablePadButton_Click(object sender, RoutedEventArgs e)
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

                        // Set affirmation and voter strings
                        string affText = "I, " + Voter.FirstName + " " + Voter.LastName + " confirm that I am a Registered Voter and to my knowledge have not cast a ballot in this election.";
                        string userText = Voter.FirstName + " " + Voter.LastName + " Party: " + Voter.Party + " Birth Year: " + Voter.DOBYear;

                        // Save voter signature from sigPad then display image on the page

                        if (await Task.Run(()=>SignatureMethods.SaveSignatureFromPad(Voter, Folder, (int)Settings.SiteID, Settings.SiteName, affText, userText)))
                        {
                            LoadSignature();

                            EnablePadClick?.Invoke(sender, e);
                        }
                        else
                        {
                            EnablePad.IsEnabled = true;
                            VoterSignature.Source = null;
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Voter != null)
            {
                if (Folder != null)
                {
                    // Delete existing signature file
                    //DeleteExistingFile();

                    DeleteClick?.Invoke(sender, e);

                    //EnablePad.IsEnabled = true;
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
    }
}
