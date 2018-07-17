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
            mSourcePath = EVPath.Normalize(ConfigurationManager.AppSettings["sourcePath"]);
            mDestPath = EVPath.Normalize(ConfigurationManager.AppSettings["destPath"]);
            mApiReferenceConfigPath = EVPath.Normalize(ConfigurationManager.AppSettings["apiReferenceConfigPath"]);

            mApiReferenceSourcePath = Path.GetDirectoryName(mApiReferenceConfigPath);
            mApiReferenceTargetPath = mApiReferenceSourcePath.Replace(@"siteSource\", string.Empty);

            ReadSiteSettings();
        }

        public const string IgnoreDestDir = "siteSource";

        public const string IgnoreGitDir = ".git";

        private static string mSourcePath = string.Empty;

        private static string mApiReferenceConfigPath;
        private static string mApiReferenceSourcePath;
        private static string mApiReferenceTargetPath;

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

        public static string ApiReferenceConfigPath
        {
            get
            {
                return mApiReferenceConfigPath;
            }
        }

        public static string ApiReferenceSourcePath
        {
            get
            {
                return mApiReferenceSourcePath;
            }
        }

        public static string ApiReferenceTargetPath
        {
            get
            {
                return mApiReferenceTargetPath;
            }
        }

        private static site mSiteSettings;

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
