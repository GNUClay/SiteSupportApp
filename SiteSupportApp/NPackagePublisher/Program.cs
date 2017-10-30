using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPackagePublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var prg = new Program();
            prg.Run(args);
        }

        public void Run(string[] args)
        {
            mArgs = args;

            if(mArgs.Count() == 0)
            {
                return;
            }

            PreparareParams();
            LoadConfigInfo();
            FindNugetExe();

            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"Run item.Key = {item.Key} item.Value = {item.Value}");
            }
        }

        private string[] mArgs;
        private string mRootFilePath;
        private string mRootDirectory;
        private ConfigInfo mConfigInfo;
        private string mNugetFileName = "nuget.exe";
        private string mNugetExePath;

        private void PreparareParams()
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"Run args.Count() = {mArgs.Count()}");

            foreach (var arg in mArgs)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"Run arg = {arg}");
            }

            mRootFilePath = Path.GetFullPath(mArgs.First());
            mRootDirectory = Path.GetDirectoryName(mRootFilePath);

            NLog.LogManager.GetCurrentClassLogger().Info($"Run mRootFilePath = {mRootFilePath}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run mRootDirectory = {mRootDirectory}");
        }

        private void LoadConfigInfo()
        {
            using (var tmpfile = new FileStream(mRootFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(tmpfile))
                {
                    mConfigInfo = JsonConvert.DeserializeObject<ConfigInfo>(reader.ReadToEnd());
                }
            }
                
            foreach (var arg in mConfigInfo.items)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"Run arg = {arg}");
            }
        }

        private void FindNugetExe(string initPath = null)
        {
            //NLog.LogManager.GetCurrentClassLogger().Info($"FindNugetExe initPath = {initPath}");

            if(!string.IsNullOrWhiteSpace(mNugetExePath))
            {
                return;
            }

            if(string.IsNullOrWhiteSpace(initPath))
            {
                initPath = $"{Environment.GetEnvironmentVariable("SystemDrive")}/";
            }

            //NLog.LogManager.GetCurrentClassLogger().Info($"FindNugetExe after initPath = {initPath}");

            try
            {
                var curentDirectory = new DirectoryInfo(initPath);

                foreach (var childrenFile in curentDirectory.EnumerateFiles())
                {
                    //NLog.LogManager.GetCurrentClassLogger().Info($"FindNugetExe childrenFile.Name = {childrenFile.Name} childrenFile.FullName = {childrenFile.FullName}");

                    if (childrenFile.Name == mNugetFileName)
                    {
                        mNugetExePath = childrenFile.FullName;
                        return;
                    }
                }

                foreach (var childrenDirectory in curentDirectory.EnumerateDirectories())
                {
                    FindNugetExe(childrenDirectory.FullName);

                    if (!string.IsNullOrWhiteSpace(mNugetExePath))
                    {
                        return;
                    }
                }
            }
            catch
            {
            }            
        }
    }
}
