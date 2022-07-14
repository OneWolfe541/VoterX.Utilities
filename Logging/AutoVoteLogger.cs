using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVote.Logging
{
    public class AutoVoteLogger
    {
        public string FileName { get; set; }

        public bool Logging { get; set; }

        public AutoVoteLogger()
        {
            Logging = false;
        }

        public AutoVoteLogger(string File, bool EnableLogging)
        {
            FileName = File;
            Logging = EnableLogging;
        }

        public void WriteLog(string Message)
        {
            if (Logging == true)
            {
                var result = WriteLogToFile(FileName, Message);
            }
        }

        public void WriteLog(Exception exception)
        {
            if (Logging == true)
            {
                //var result = WriteLogToFile(FileName, exception.Message);
                if(exception.InnerException != null)
                {
                    var result = WriteLogToFileNoDate(FileName, exception.InnerException.Message);
                    if (exception.InnerException.InnerException != null)
                    {
                        result = WriteLogToFileNoDate(FileName, exception.InnerException.InnerException.Message);
                    }
                }
                else
                {
                    var result = WriteLogToFileNoDate(FileName, exception.Message);
                }
            }
        }

        public void WriteLog(string Message, Exception exception)
        {
            if (Logging == true)
            {
                var result = WriteLogToFile(FileName, Message + " " + exception.Message);
                if (exception.InnerException != null)
                {
                    result = WriteLogToFileNoDate(FileName, exception.InnerException.Message);
                    if (exception.InnerException.InnerException != null)
                    {
                        result = WriteLogToFileNoDate(FileName, exception.InnerException.InnerException.Message);
                    }
                }
            }
        }

        private bool WriteLogToFile(string FileName, string Message)
        {
            bool result = false;
            try
            {
                string subPath = "C:\\AutoVote\\Debug\\";

                System.IO.Directory.CreateDirectory(subPath);

                File.AppendAllText(subPath + FileName + ".log", DateTime.Now.ToString() + " " + Message + "\r\n");

                result = true;
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return result;
        }

        private bool WriteLogToFileNoDate(string FileName, string Message)
        {
            bool result = false;
            try
            {
                string subPath = "C:\\AutoVote\\Debug\\";

                System.IO.Directory.CreateDirectory(subPath);

                File.AppendAllText(subPath + FileName + ".log", "\t\t " + Message + "\r\n");

                result = true;
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return result;
        }
    }
}
