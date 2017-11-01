using CommonUtils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
                PipelineMessages.WriteLine("There are not command line any arguments! :(");
                return;
            }

            PreparareParams();
            GetApiKey();
            LoadConfigInfo();
            FindNugetExe();
            Pack();
            Push();
            Clear();

            PipelineMessages.WriteLine("The publication was successfull! :)");
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
            PipelineMessages.WriteLine("Prepare arguments ...");

            mRootFilePath = Path.GetFullPath(mArgs.First());
            mRootDirectory = Path.GetDirectoryName(mRootFilePath);

            PipelineMessages.InitResponseFile(Path.Combine(mRootDirectory, "result.txt"));
        }

        private void GetApiKey()
        {
            Console.Write("Please! Enter your nuget API key here:");
            mApiKey = Console.ReadLine();

            PipelineMessages.WriteLine("");

            NLog.LogManager.GetCurrentClassLogger().Info($"Run mApiKey = {mApiKey}");
        }

        private void LoadConfigInfo()
        {
            PipelineMessages.WriteLine("Begin loading configInfo...");

            using (var tmpfile = new FileStream(mRootFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(tmpfile))
                {
                    mConfigInfo = JsonConvert.DeserializeObject<ConfigInfo>(reader.ReadToEnd());
                }
            }

            if (mConfigInfo.items.Count == 0)
            {
                throw new BusinessLogicException();
            }

            foreach (var item in mConfigInfo.items)
            {
                var packageInfo = new PackageInfo();
                packageInfo.Name = item;
                packageInfo.Path = Path.Combine(mRootDirectory, item);
                packageInfo.NuspecName = Path.Combine(packageInfo.Path, $"{item}.nuspec");
                packageInfo.ProjectName = Path.Combine(packageInfo.Path, $"{item}.csproj");
                packageInfo.Version = VersionWorker.GetVersion(packageInfo.Path);
                packageInfo.ShortVersion = VersionWorker.GetShortVersion(packageInfo.Version);
                packageInfo.PackageName = $"{item}.{packageInfo.Version}.nupkg";
                packageInfo.PackageFullName = Path.Combine(packageInfo.Path, packageInfo.PackageName);

                mPackageInfoList.Add(packageInfo);
            }

            PipelineMessages.WriteLine("All of items of configinfo have loaded successfully! :)");
        }

        private void FindNugetExe()
        {
            PipelineMessages.WriteLine("Find nuget.exe ...");

            var initPath = EVPath.Normalize(ConfigurationManager.AppSettings["NuGetPath"]);

            if(!string.IsNullOrWhiteSpace(initPath))
            {
                if(File.Exists(initPath))
                {
                    mNugetExePath = initPath;
                }       
            }

            initPath = $"{Environment.GetEnvironmentVariable("SystemDrive")}/";

            FindNugetExe(initPath);

            if(string.IsNullOrWhiteSpace(mNugetExePath))
            {
                throw new BusinessLogicException();
            }

            PipelineMessages.WriteLine("The nuget.exe have found successfully! :)");
        }

        private void FindNugetExe(string initPath)
        {
            if(!string.IsNullOrWhiteSpace(mNugetExePath))
            {
                return;
            }

            try
            {
                var curentDirectory = new DirectoryInfo(initPath);

                foreach (var childrenFile in curentDirectory.EnumerateFiles())
                {
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
            PipelineMessages.WriteLine("Pack items ...");

            foreach (var packageInfo in mPackageInfoList)
            {
                Directory.SetCurrentDirectory(packageInfo.Path);

                var process = Process.Start(mNugetExePath, $" pack {packageInfo.ProjectName}");
                process.WaitForExit();

                if(process.ExitCode != 0)
                {
                    throw new BusinessLogicException();
                }
            }

            PipelineMessages.WriteLine("All of items have packed successfully! :)");
        }

        private void Push()
        {
            PipelineMessages.WriteLine("Push items ...");

            foreach (var packageInfo in mPackageInfoList)
            {
                Directory.SetCurrentDirectory(packageInfo.Path);

                var process = Process.Start(mNugetExePath, $" push {packageInfo.PackageName} {mApiKey}");
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new BusinessLogicException();
                }

                var targetLink = $"https://www.nuget.org/packages/{packageInfo.Name}/{packageInfo.ShortVersion}";

                PipelineMessages.WriteLine($"Uploaded package:{targetLink}");
            }

            PipelineMessages.WriteLine("All of items have pushed successfully! :)");
        }

        private void Clear()
        {
            PipelineMessages.WriteLine("Clear items ...");

            foreach (var packageInfo in mPackageInfoList)
            {
                File.Delete(packageInfo.PackageFullName);
            }

            PipelineMessages.WriteLine("All of items have cleaned successfully! :)");
        }
    }
}
