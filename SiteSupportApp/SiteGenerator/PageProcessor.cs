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
            NLog.LogManager.GetCurrentClassLogger().Info("Run");
            NLog.LogManager.GetCurrentClassLogger().Info("info.SourceName = {0}", info.SourceName);
            NLog.LogManager.GetCurrentClassLogger().Info("info.TargetDir = {0}", info.TargetDirName);
            NLog.LogManager.GetCurrentClassLogger().Info("info.FileNameWithOutExtension = {0}", info.FileNameWithOutExtension);
            
            var tmpSitePage = sitePage.LoadFromFile(info.SourceName);

            NLog.LogManager.GetCurrentClassLogger().Info("tmpSitePage.contentPath = {0}", tmpSitePage.contentPath);
            NLog.LogManager.GetCurrentClassLogger().Info("tmpSitePage.extension = {0}", tmpSitePage.extension);
            NLog.LogManager.GetCurrentClassLogger().Info("tmpSitePage.title = {0}", tmpSitePage.title);

            if (string.IsNullOrWhiteSpace(tmpSitePage.contentPath))
            {
                NLog.LogManager.GetCurrentClassLogger().Info("Run FindContent Path");

                tmpSitePage.contentPath = Path.Combine(Path.GetDirectoryName(info.SourceName), Path.GetFileNameWithoutExtension(info.SourceName) + ".html");
            }

            NLog.LogManager.GetCurrentClassLogger().Info("tmpSitePage.contentPath = {0}", tmpSitePage.contentPath);

            var tmpPage = new TargetPage();

            tmpPage.Title = tmpSitePage.title;

            using (var tmpTextReader = new StreamReader(tmpSitePage.contentPath))
            {
                tmpPage.Content = tmpTextReader.ReadToEnd();
            }

            tmpPage.Run(Path.Combine(info.TargetDirName, Path.GetFileNameWithoutExtension(info.SourceName) +"." + tmpSitePage.extension));
        }
    }
}
