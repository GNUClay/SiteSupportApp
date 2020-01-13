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

using System.Configuration;
using System.IO;
using CommonSiteGeneratorLib.SiteData;
using CommonUtils;

namespace CommonSiteGeneratorLib
{
    public static class GeneralSettings
    {
        static GeneralSettings()
        {
            SiteName = ConfigurationManager.AppSettings["siteName"];

            //var tmpRootPath = EVPath.Normalize(ConfigurationManager.AppSettings["rootPath"]);
            var tmpRootPath = ConfigAppSettingsHelper.GetExistingDirectoryName("rootPath");

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"GeneralSettings() tmpRootPath = {tmpRootPath}");
#endif

            EVPath.RegVar("SITE_ROOT_PATH", tmpRootPath);

            SourcePath = EVPath.Normalize(ConfigurationManager.AppSettings["sourcePath"]);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"GeneralSettings() SourcePath = {SourcePath}");
#endif

            EVPath.RegVar("SITE_SOURCE_PATH", SourcePath);

            DestPath = EVPath.Normalize(ConfigurationManager.AppSettings["destPath"]);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"GeneralSettings() DestPath = {DestPath}");
            NLog.LogManager.GetCurrentClassLogger().Info($"ConfigurationManager.AppSettings['tempPath'] = {ConfigurationManager.AppSettings["tempPath"]}");
#endif

            EVPath.RegVar("SITE_DEST_PATH", DestPath);

            var initTempPath = ConfigurationManager.AppSettings["tempPath"];

            if(!string.IsNullOrWhiteSpace(initTempPath))
            {
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"GeneralSettings() initTempPath = {initTempPath}");
#endif

                TempPath = EVPath.Normalize(initTempPath);

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"GeneralSettings() TempPath = {TempPath}");
#endif

                EVPath.RegVar("SITE_TEMP_PATH", TempPath);

                if (!Directory.Exists(TempPath))
                {
                    Directory.CreateDirectory(TempPath);
                }
            }

            var initApiReferenceConfigPath = ConfigurationManager.AppSettings["apiReferenceConfigPath"];

            if(!string.IsNullOrWhiteSpace(initApiReferenceConfigPath))
            {
                ApiReferenceConfigPath = EVPath.Normalize(initApiReferenceConfigPath);

                ApiReferenceSourcePath = Path.GetDirectoryName(ApiReferenceConfigPath);
                ApiReferenceTargetPath = ApiReferenceSourcePath.Replace(@"siteSource\", string.Empty);
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"ApiReferenceConfigPath = {ApiReferenceConfigPath}");
#endif

            ReadSiteSettings();
        }

        public const string IgnoreDestDir = "siteSource";

        public const string IgnoreGitDir = ".git";
        public const string VSDir = ".vs";

        public static string SiteName { get; private set; } = string.Empty;

        public static string SourcePath { get; private set; } = string.Empty;

        public static string DestPath { get; private set; } = string.Empty;

        public static string TempPath { get; private set; } = string.Empty;

        public static string ApiReferenceConfigPath { get; private set; }

        public static string ApiReferenceSourcePath { get; private set; }

        public static string ApiReferenceTargetPath { get; private set; }

        public static SiteInfo SiteSettings { get; private set; }

        private static void ReadSiteSettings()
        {
            var tmpSiteSettingsPath = Path.Combine(SourcePath, "site.site");

            SiteSettings = SiteInfo.LoadFromFile(tmpSiteSettingsPath);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"SiteSettings = {SiteSettings}");
#endif
        }
    }
}
