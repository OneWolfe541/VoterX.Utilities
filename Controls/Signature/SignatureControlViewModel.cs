using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using VoterX.Core.Voters;
using VoterX.SystemSettings.Models;
using VoterX.Utilities.Commands;
using VoterX.Utilities.Methods;

namespace VoterX.Utilities.Controls
{
    public class SignatureControlViewModel : NotifyPropertyChanged
    {
        private NMVoter _voter;
        private string _folder;
        private SystemSettingsModel _settings;        

        public SignatureControlViewModel(NMVoter voter, string folder, SystemSettingsModel settings)
        {
            _voter = voter;
            _folder = folder;
            _settings = settings;
        }

        public BitmapImage SignatureImage
        {
            get
            {
                if (_voter != null)
                {
                    if (_folder != null)
                    {
                        BitmapImage image = SignatureMethods.LoadSignatureFromFile(_voter.Data, _folder);
                        if (image == null)
                        {
                            CanEnablePadClick = true;
                            CanDeleteSignatureClick = false;
                        }
                        else
                        {
                            CanEnablePadClick = false;
                            CanDeleteSignatureClick = true;
                        }
                        return image;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        #region Commands
        // Examples of how to get the button to work inside the ListBox
        // https://stackoverflow.com/questions/4500729/how-to-use-binding-in-the-listbox-s-items-to-the-viewmodel-s-properties
        // relayCommand from the John Smith articles
        // https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
        // https://stackoverflow.com/questions/51845335/how-to-bind-command-when-button-inside-of-listbox-item
        public RelayCommand _enablePadClickCommand;
        public ICommand EnablePadClickCommand
        {
            get
            {
                if (_enablePadClickCommand == null)
                {
                    _enablePadClickCommand = new RelayCommand(param => this.EnablePadClick(), param => this.CanEnablePadClick);
                }
                return _enablePadClickCommand;
            }
        }

        private bool _canEnablePadClick;
        public bool CanEnablePadClick
        {
            get { return _canEnablePadClick; }
            internal set
            {
                _canEnablePadClick = value;
                RaisePropertyChanged("CanEnablePadClick");
            }
        }

        public async void EnablePadClick()
        {
            CanEnablePadClick = false;

            if (_voter != null)
            {
                if (_folder != null)
                {
                    if (_settings != null)
                    {
                        // Delete existing signature file
                        DeleteExistingFile();

                        // Set affirmation and voter strings
                        string affText = "I, " + _voter.Data.FirstName + " " + _voter.Data.LastName + " confirm that I am a Registered Voter and to my knowledge have not cast a ballot in this election.";
                        string userText = _voter.Data.FirstName + " " + _voter.Data.LastName + " Party: " + _voter.Data.Party + " Birth Year: " + _voter.Data.DOBYear;

                        // Save voter signature from sigPad then display image on the page

                        if (await Task.Run(() => SignatureMethods.SaveSignatureFromPad(_voter.Data, _folder, (int)_settings.SiteID, _settings.SiteName, affText, userText)))
                        {
                            //LoadSignature();
                            RaisePropertyChanged("SignatureImage");

                            //EnablePadClick?.Invoke(sender, e);
                        }
                        else
                        {
                            CanEnablePadClick = true;
                            //VoterSignature.Source = null;
                        }
                    }
                    else
                    {
                        //EnablePad.IsEnabled = true;
                        //throw new CustomException("Settings Not Set!");
                        CanEnablePadClick = true;
                    }
                }
                else
                {
                    //EnablePad.IsEnabled = true;
                    //throw new CustomException("Folder Not Set!");
                    CanEnablePadClick = true;
                }
            }
            else
            {
                //EnablePad.IsEnabled = true;
                //throw new CustomException("Voter Not Set!");
                CanEnablePadClick = true;
            }
        }

        public RelayCommand _deleteSignatureClickCommand;
        public ICommand DeleteSignatureClickCommand
        {
            get
            {
                if (_deleteSignatureClickCommand == null)
                {
                    _deleteSignatureClickCommand = new RelayCommand(param => this.DeleteExistingFile(), param => this.CanDeleteSignatureClick);
                }
                return _deleteSignatureClickCommand;
            }
        }

        private bool _canDeleteSignatureClick;
        public bool CanDeleteSignatureClick
        {
            get { return _canDeleteSignatureClick; }
            internal set
            {
                _canDeleteSignatureClick = value;
                RaisePropertyChanged("CanDeleteSignatureClick");
            }
        }

        // Clear the image and delete the existing file
        private void DeleteExistingFile()
        {
            //LoggingMethods.LogIO("DELETE SIGNATURE IMAGE");

            // Clear the image control
            //VoterSignature.Source = null;

            // Display which file is being deleted
            //StatusBar.ApplicationStatus("Searching For: " + voter.VoterID.ToString());

            if (SignatureMethods.DeleteVoterSignature(_voter.Data, _folder))
            {
                //StatusBar.ApplicationStatus("File Deleted: " + voter.VoterID.ToString());
                //Error = "File Deleted: " + _voter.Data.VoterID.ToString();

                // Update Image
                RaisePropertyChanged("SignatureImage");

                // Disable Clear Button
                //CanDeleteSignatureClick = false;
                //RaisePropertyChanged("CanDeleteSignatureClick");

                // Enable Sig Pad Button
            }
            else
            {
                //StatusBar.ApplicationStatus("File not found: " + voter.VoterID.ToString());
                //Error = "File not found: " + Voter.VoterID.ToString();
            }
        }

        #endregion
    }
}
