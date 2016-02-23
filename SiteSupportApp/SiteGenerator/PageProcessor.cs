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

using System.IO;
namespace SiteGenerator
{
    public class PageProcessor
    {
        public class PageNodeInfo
        {
            public string SourceName = string.Empty;
            public string TargetDirName = string.Empty;
            public string FileNameWithOutExtension = string.Empty;
        }

        public static void Run(PageNodeInfo info)
        {         
            var tmpSitePage = sitePage.LoadFromFile(info.SourceName);

            if (string.IsNullOrWhiteSpace(tmpSitePage.contentPath))
            {
                tmpSitePage.contentPath = Path.Combine(Path.GetDirectoryName(info.SourceName), Path.GetFileNameWithoutExtension(info.SourceName) + ".thtml");
            }

            if (string.IsNullOrWhiteSpace(tmpSitePage.title))
            {
                tmpSitePage.title = GeneralSettings.SiteSettings.title;
            }

            var tmpPage = new TargetPage();

            tmpPage.Title = tmpSitePage.title;

            var tmpFileInfo = new FileInfo(tmpSitePage.contentPath);

            tmpPage.LastUpdateDate = tmpFileInfo.LastWriteTime;

            using (var tmpTextReader = new StreamReader(tmpSitePage.contentPath))
            {
                tmpPage.Content = tmpTextReader.ReadToEnd();
            }

            tmpPage.Run(Path.Combine(info.TargetDirName, Path.GetFileNameWithoutExtension(info.SourceName) +"." + tmpSitePage.extension));
        }
    }
}
