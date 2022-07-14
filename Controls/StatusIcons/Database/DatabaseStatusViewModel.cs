using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VoterX.Core.Elections;
using VoterX.Utilities.Methods;
using VoterXVCC;

namespace VoterX.Utilities.Controls
{
    public class DatabaseStatusViewModel : StatusIconControlBase
    {
        private readonly string _connection;
        private int _waitTime;

        public DatabaseStatusViewModel(string ConnectionString) : this(ConnectionString, null) { }
        public DatabaseStatusViewModel(string ConnectionString, int? DefaultWaitTime)
        {
            DefaultIconName = "Database";
            ControlName = "Database";

            _connection = ConnectionString;
            _waitTime = DefaultWaitTime == null ? 500 : (int)DefaultWaitTime;
        }

        #region CheckConnection
        public void CheckConnection()
        {
            CheckConnection(null);
        }
        public async void CheckConnection(int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Create disposable DB context factory
            using (ElectionFactory electionFactory = new ElectionFactory())
            {
                // Wait 1 second
                await AsyncUtilities.PutTaskDelay(_waitTime);

                // Check if context exists
                if (await electionFactory.ExistsAsync() == true)
                {
                    // Wait 1 second
                    await AsyncUtilities.PutTaskDelay(_waitTime);

                    StatusOkIcon = true;
                }
                else
                {
                    StatusBadIcon = true;
                }
            }
        }
        public async Task<bool> CheckConnectionAsync(int? WaitTime)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Create disposable DB context factory
            using (ElectionFactory electionFactory = new ElectionFactory())
            {
                // Wait 1 second
                await AsyncUtilities.PutTaskDelay(_waitTime);

                // Check if context exists
                if (await electionFactory.ExistsAsync() == true)
                {
                    // Wait 1 second
                    await AsyncUtilities.PutTaskDelay(_waitTime);

                    StatusOkIcon = true;
                    return true;
                }
                else
                {
                    StatusBadIcon = true;
                    return false;
                }
            }
            //return false;
        }
        public async Task<bool> CheckConnectionAsync(int? WaitTime, string conneciton)
        {
            // Override wait time
            _waitTime = WaitTime == null ? _waitTime : (int)WaitTime;

            // Start loading animation
            StatusSpinnerIcon = true;

            // Create disposable DB context factory
            using (ElectionFactory electionFactory = new ElectionFactory())
            {
                // Wait 1 second
                await AsyncUtilities.PutTaskDelay(_waitTime);

                // Check if context exists
                if (await electionFactory.ExistsAsync(conneciton) == true)
                {
                    // Wait 1 second
                    await AsyncUtilities.PutTaskDelay(_waitTime);

                    StatusOkIcon = true;
                    return true;
                }
                else
                {
                    StatusBadIcon = true;
                    return false;
                }
            }
            //return false;
        }
        #endregion

    }
}
