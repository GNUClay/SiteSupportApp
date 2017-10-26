/* SiteSupportApp supports generating the web site <http://gnuclay.github.io>
*  Copyright (c) 2016 metatypeman
*  <https://github.com/GNUClay/SiteSupportApp.git>
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SiteGenerator
{
    public static class GeneralSettings
    {
        public const string CommonProgramFiles_x86 = "$(CommonProgramFiles(x86))";
        //OneDrive
        //TMP
        //TEMP
        //LOCALAPPDATA
        //$(PUBLIC)
        //ProgramFiles(x86)
        //CommonProgramFiles
        //VisualStudioDir
        //ProgramData
        //ProgramW6432
        //ProgramFiles
        //SystemRoot
        //CommonProgramW6432
        //USERPROFILE
        //APPDATA
        //HOMEDRIVE
        //SystemDrive
        //windir
        //ALLUSERSPROFILE
        private static Dictionary<string, string> mVarConstDict = new Dictionary<string, string>();

        private static void InitVarConstDict()
        {
            mVarConstDict[CommonProgramFiles_x86] = "CommonProgramFiles(x86)";

        }

        static GeneralSettings()
        {
            InitVarConstDict();

            mSourcePath = ResolvePath(ConfigurationManager.AppSettings["sourcePath"]);

            NLog.LogManager.GetCurrentClassLogger().Info($"mSourcePath = {mSourcePath}");

            mDestPath = ResolvePath(ConfigurationManager.AppSettings["destPath"]);

            NLog.LogManager.GetCurrentClassLogger().Info($"mDestPath = {mDestPath}");

            ReadSiteSettings();
        }

        private static string ResolvePath(string source)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"ResolvePath source = '{source}'");

            if(source.StartsWith(CommonProgramFiles_x86))
            {
                return ReplaceConstInPath(source, CommonProgramFiles_x86);
            }

            return source;
        }

        private static string ReplaceConstInPath(string source, string varName)
        {
            var variableName = mVarConstDict[varName];
            var value = Environment.GetEnvironmentVariable(variableName);

            NLog.LogManager.GetCurrentClassLogger().Info($"ResolvePath value = '{value}'");

            source = source.Replace(varName, string.Empty);

            if(source.StartsWith("/"))
            {
                source = source.Substring(1);
            }

            NLog.LogManager.GetCurrentClassLogger().Info($"ResolvePath source = '{source}'");

            return Path.Combine(value, source);
        }

        public const string IgnoreDestDir = "siteSource";

        public const string IgnoreGitDir = ".git";

        private static string mSourcePath = string.Empty;

        public static string SourcePath
        {
            get
            {
                return mSourcePath;
            }
        }

        private static string mDestPath = string.Empty;

        public static string DestPath
        {
            get
            {
                return mDestPath;
            }
        }

        private static site mSiteSettings = null;

        public static site SiteSettings
        {
            get
            {
                return mSiteSettings;
            }
        }

        private static void ReadSiteSettings()
        {
            var tmpSiteSettingsPath = Path.Combine(SourcePath, "site.site");

            mSiteSettings = site.LoadFromFile(tmpSiteSettingsPath);
        }
    }
}
