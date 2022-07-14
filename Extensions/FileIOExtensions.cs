using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterX.Utilities.Extensions
{
    public static class FileIOExtensions
    {
        public static async Task<FileStream> OpenReadAsync(string path)
        {
            try
            {
                return await Task.Run(() => OpenRead(path));
            }
            catch (Exception e)
            {
                throw e;
            }

            //return await Task.Run(() =>
            //{
            //    File.OpenRead(path)
            //    }
            //);
        }

        private static FileStream OpenRead(string path)
        {
            try
            {
                return File.OpenRead(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
