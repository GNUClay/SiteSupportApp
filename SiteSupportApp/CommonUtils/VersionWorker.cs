using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class VersionWorker
    {
        public static string GetVersion(string projectPath)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion projectPath = {projectPath}");

            var properyDir = Path.Combine(projectPath, "Properties");

            NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion properyDir = {properyDir}");

            var assemblyInfoFileName = Path.Combine(properyDir, "AssemblyInfo.cs");

            NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion assemblyInfoFileName = {assemblyInfoFileName}");

            using (var tmpfile = new FileStream(assemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(tmpfile))
                {
                    var content = reader.ReadToEnd();

                    NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion content = {content}");

                    var match = Regex.Match(content, "AssemblyFileVersion\\(\"\\d+.\\d+.\\d+.\\d+\"\\)");

                    NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion match = {match}");

                    if(match.Success)
                    {
                        var versionContent = match.Value;

                        var match2 = Regex.Match(versionContent, "\\d+.\\d+.\\d+.\\d+");

                        NLog.LogManager.GetCurrentClassLogger().Info($"GetVersion match2 = {match2}");
                    }
                }
            }

            return string.Empty;
        }
    }
}
