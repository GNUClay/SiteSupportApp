using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class EVPath
    {
        private static Regex mNormalizeMatch = new Regex("(%(\\w|\\(|\\))+%)");
        private static Regex mNormalizeMatch2 = new Regex("(\\w|\\(|\\))+");

        public static string Normalize(string sourcePath)
        {
            var match = mNormalizeMatch.Match(sourcePath);

            if (match.Success)
            {
                var targetValue = match.Value;

                var match2 = mNormalizeMatch2.Match(targetValue);

                if (match2.Success)
                {
                    var variableName = match2.Value;

                    var variableValue = string.Empty;

                    if(mAdditionalVariablesDict.ContainsKey(variableName))
                    {
                        variableValue = mAdditionalVariablesDict[variableName];
                    }
                    else
                    {
                        variableValue = Environment.GetEnvironmentVariable(variableName);
                    }

                    if (!string.IsNullOrWhiteSpace(variableValue))
                    {
                        sourcePath = sourcePath.Replace(targetValue, variableValue);
                    }
                }
            }

            return Path.GetFullPath(sourcePath);
        }

        public static void RegVar(string varName, string varValue)
        {
            mAdditionalVariablesDict[varName] = varValue;
        }

        private static readonly Dictionary<string, string> mAdditionalVariablesDict = new Dictionary<string, string>();
    }
}
