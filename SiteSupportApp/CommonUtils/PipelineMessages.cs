using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class PipelineMessages
    {
        private static FileStream mFileStream;
        private static StreamWriter mStreamWriter;
        
        public static void InitResponseFile(string fileName)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"InitResponseFile fileName = {fileName}");

            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            mFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            mStreamWriter = new StreamWriter(mFileStream);
            mStreamWriter.AutoFlush = true;
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);

            if(mStreamWriter != null)
            {
                mStreamWriter.WriteLine(text);
            }
        }
    }
}
