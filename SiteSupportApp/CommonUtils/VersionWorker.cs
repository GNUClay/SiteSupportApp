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
        private static Regex mGetVersionRegex = new Regex("AssemblyFileVersion\\(\"\\d+.\\d+.\\d+.\\d+\"\\)");
        private static Regex mGetVersionRegex2 = new Regex("\\d+.\\d+.\\d+.\\d+");
        private static Regex mGetVersionShortRegex2 = new Regex("\\d+.\\d+.\\d+");

        public static string GetVersion(string projectPath)
        {
            var properyDir = Path.Combine(projectPath, "Properties");
            var assemblyInfoFileName = Path.Combine(properyDir, "AssemblyInfo.cs");

            using (var tmpfile = new FileStream(assemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(tmpfile))
                {
                    var content = reader.ReadToEnd();
                    var match = mGetVersionRegex.Match(content);

                    if(match.Success)
                    {
                        var versionContent = match.Value;
                        var match2 = mGetVersionRegex2.Match(versionContent);
                        return match2.Value;
                    }
                }
            }

            return string.Empty;
        }

        public static string GetShortVersion(string longVersion)
        {
            var match = mGetVersionShortRegex2.Match(longVersion);

            if (match.Success)
            {
                return match.Value;
            }

            return longVersion;
        }
    }
}
