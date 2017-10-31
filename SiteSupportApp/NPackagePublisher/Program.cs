using CommonUtils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            GetApiKey();
            LoadConfigInfo();
            FindNugetExe();
            Pack();
            Push();
            Clear();

            NLog.LogManager.GetCurrentClassLogger().Info($"Run mNugetExePath = {mNugetExePath}");

            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"Run item.Key = {item.Key} item.Value = {item.Value}");
            }
        }

        private string[] mArgs;
        private string mApiKey;
        private string mRootFilePath;
        private string mRootDirectory;
        private ConfigInfo mConfigInfo;
        private List<PackageInfo> mPackageInfoList = new List<PackageInfo>();
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

        private void GetApiKey()
        {
            Console.Write("Please! Enter your nuget API key here:");
            mApiKey = Console.ReadLine();

            NLog.LogManager.GetCurrentClassLogger().Info($"Run mApiKey = {mApiKey}");
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

            foreach (var item in mConfigInfo.items)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo item = {item}");

                var packageInfo = new PackageInfo();
                packageInfo.Name = item;
                packageInfo.Path = Path.Combine(mRootDirectory, item);
                packageInfo.NuspecName = Path.Combine(packageInfo.Path, $"{item}.nuspec");
                packageInfo.ProjectName = Path.Combine(packageInfo.Path, $"{item}.csproj");

                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.Path = {packageInfo.Path}");
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.NuspecName = {packageInfo.NuspecName}");
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.ProjectName = {packageInfo.ProjectName}");

                packageInfo.Version = VersionWorker.GetVersion(packageInfo.Path);
                packageInfo.PackageName = $"{item}.{packageInfo.Version}.nupkg";
                packageInfo.PackageFullName = Path.Combine(packageInfo.Path, packageInfo.PackageName);
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.Version = {packageInfo.Version}");
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.PackageName = {packageInfo.PackageName}");
                NLog.LogManager.GetCurrentClassLogger().Info($"LoadConfigInfo packageInfo.PackageFullName = {packageInfo.PackageFullName}");

                mPackageInfoList.Add(packageInfo);
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

        private void Pack()
        {
            foreach(var packageInfo in mPackageInfoList)
            {
                Directory.SetCurrentDirectory(packageInfo.Path);

                NLog.LogManager.GetCurrentClassLogger().Info($"Pack currDir = `{Directory.GetCurrentDirectory()}`");

                var process = Process.Start(mNugetExePath, $" pack {packageInfo.ProjectName}");
                process.WaitForExit();

                if(process.ExitCode != 0)
                {
                    throw new NotSupportedException();
                }
            }
        }

        private void Push()
        {
            foreach (var packageInfo in mPackageInfoList)
            {
                Directory.SetCurrentDirectory(packageInfo.Path);

                var process = Process.Start(mNugetExePath, $" push {packageInfo.ProjectName} {mApiKey}");
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new NotSupportedException();
                }
            }
        }

        private void Clear()
        {
            foreach (var packageInfo in mPackageInfoList)
            {
                File.Delete(packageInfo.PackageFullName);
            }
        }
    }
}
