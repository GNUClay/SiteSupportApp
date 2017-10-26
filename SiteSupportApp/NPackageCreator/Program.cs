using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPackageCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            CreatePackage("GnuClay.CommonUtils");
            CreatePackage("GnuClay.CommonClientTypes");
            CreatePackage("GnuClay.Engine");
            CreatePackage("GnuClay.LocalHost");
        }

        private static string RootPath;
        private static string ProgramRootPath;
        private static string NuGetStr;

        private static void Init()
        {
            var currDir = Directory.GetCurrentDirectory();

            NLog.LogManager.GetCurrentClassLogger().Info($"currDir = `{currDir}`");

            var tmpParent = Directory.GetParent(currDir).Parent;

            ProgramRootPath = tmpParent.FullName;

            NuGetStr = Path.Combine(ProgramRootPath, "nuget.exe");

            RootPath = tmpParent.Parent.FullName;

            NLog.LogManager.GetCurrentClassLogger().Info($"ProgramRootPath = `{ProgramRootPath}`");
            NLog.LogManager.GetCurrentClassLogger().Info($"NuGetStr = `{NuGetStr}`");
            NLog.LogManager.GetCurrentClassLogger().Info($"RootPath = `{RootPath}`");
        }

        private static void CreatePackage(string projectName)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"CreatePackage projectName = {projectName}");

            var targetPath = Path.Combine(RootPath, projectName);

            NLog.LogManager.GetCurrentClassLogger().Info($"CreatePackage targetPath = {targetPath}");

            Directory.SetCurrentDirectory(targetPath);

            NLog.LogManager.GetCurrentClassLogger().Info($"currDir = `{Directory.GetCurrentDirectory()}`");

            var process = Process.Start(NuGetStr, $" pack {projectName}.csproj");
            process.WaitForExit();

            NLog.LogManager.GetCurrentClassLogger().Info("End CreatePackage");
        }
    }
}
