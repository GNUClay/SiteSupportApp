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
using CommonUtils;

namespace CommonSiteGeneratorLib
{
    public static class GeneralSettings
    {
        static GeneralSettings()
        {
            SiteName = ConfigurationManager.AppSettings["siteName"];
            SourcePath = EVPath.Normalize(ConfigurationManager.AppSettings["sourcePath"]);
            DestPath = EVPath.Normalize(ConfigurationManager.AppSettings["destPath"]);
            TempPath = EVPath.Normalize(ConfigurationManager.AppSettings["tempPath"]);

            var initApiReferenceConfigPath = ConfigurationManager.AppSettings["apiReferenceConfigPath"];

            if(!string.IsNullOrWhiteSpace(initApiReferenceConfigPath))
            {
                ApiReferenceConfigPath = EVPath.Normalize(initApiReferenceConfigPath);

                ApiReferenceSourcePath = Path.GetDirectoryName(ApiReferenceConfigPath);
                ApiReferenceTargetPath = ApiReferenceSourcePath.Replace(@"siteSource\", string.Empty);
            }

            ReadSiteSettings();
        }

        public const string IgnoreDestDir = "siteSource";

        public const string IgnoreGitDir = ".git";

        public static string SiteName { get; private set; } = string.Empty;

        public static string SourcePath { get; private set; } = string.Empty;

        public static string DestPath { get; private set; } = string.Empty;

        public static string TempPath { get; private set; } = string.Empty;

        public static string ApiReferenceConfigPath { get; private set; }

        public static string ApiReferenceSourcePath { get; private set; }

        public static string ApiReferenceTargetPath { get; private set; }

        public static site SiteSettings { get; private set; }

        private static void ReadSiteSettings()
        {
            var tmpSiteSettingsPath = Path.Combine(SourcePath, "site.site");

            SiteSettings = site.LoadFromFile(tmpSiteSettingsPath);
        }
    }
}
