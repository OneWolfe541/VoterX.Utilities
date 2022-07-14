using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Methods
{
    public static class AsyncUtilities
    {
        // https://stackoverflow.com/questions/22158278/wait-some-seconds-without-blocking-ui-execution
        public static async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}
