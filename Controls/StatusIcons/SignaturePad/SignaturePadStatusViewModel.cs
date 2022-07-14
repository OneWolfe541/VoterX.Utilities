using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.Methods;

namespace VoterX.Utilities.Controls
{
    public class SignaturePadStatusViewModel : StatusIconControlBase
    {
        private int _waitTime;

        public SignaturePadStatusViewModel() : this(null) { }
        public SignaturePadStatusViewModel(int? DefaultWaitTime)
        {
            DefaultIconName = "Edit";
            ControlName = "Signature Pad";

            _waitTime = DefaultWaitTime == null ? 500 : (int)DefaultWaitTime;
        }

        #region CheckSigPad
        public void CheckSignaturePad()
        {
            CheckSignaturePad(null);
        }
        public async void CheckSignaturePad(int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Check if sig pad is connected
            var result = await SignatureMethods.CheckSignaturePadAsync();

            // Wait 1 second
            await AsyncUtilities.PutTaskDelay(_waitTime);

            if (result != null)
            {
                StatusOkIcon = true;
            }
            else
            {
                StatusBadIcon = true;
            }
        }
        public async Task<bool> CheckSignaturePadAsync(int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Check if sig pad is connected
            var result = await SignatureMethods.CheckSignaturePadAsync();

            // Wait 1 second
            await AsyncUtilities.PutTaskDelay(_waitTime);

            if (result != null)
            {
                StatusOkIcon = true;
                return true;
            }
            else
            {
                StatusBadIcon = true;
                return false;
            }
        }
        #endregion
    }
}
