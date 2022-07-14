using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterX.Utilities.Methods;

namespace VoterX.Utilities.Controls
{
    public class PrinterStatusViewModel : StatusIconControlBase
    {
        private int _waitTime;

        public PrinterStatusViewModel() : this(null) { }
        public PrinterStatusViewModel(int? DefaultWaitTime)
        {
            DefaultIconName = "Print";
            ControlName = "Printer";

            _waitTime = DefaultWaitTime == null ? 500 : (int)DefaultWaitTime;
        }

        #region CheckPrinter
        public void CheckPrinter(string PrinterName)
        {
            CheckPrinter(PrinterName, null);
        }
        public async void CheckPrinter(string PrinterName, int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Check if printer is ready
            bool result = await PrinterStatus.PrinterIsReadyAsync(PrinterName);

            // Wait 1 second
            await AsyncUtilities.PutTaskDelay(_waitTime);

            if (result == true)
            {
                StatusOkIcon = true;
            }
            else
            {
                StatusBadIcon = true;
            }
        }
        public async Task<bool> CheckPrinterAsync(string PrinterName, int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Check if printer is ready
            bool result = await PrinterStatus.PrinterIsReadyAsync(PrinterName);

            // Wait 1 second
            await AsyncUtilities.PutTaskDelay(_waitTime);

            if (result == true)
            {
                StatusOkIcon = true;
            }
            else
            {
                StatusBadIcon = true;
            }
            return result;
        }
        #endregion
    }
}
