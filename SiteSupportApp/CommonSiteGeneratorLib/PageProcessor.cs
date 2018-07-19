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
using System.IO;
using System.Text;

namespace CommonSiteGeneratorLib
{
    public class PageProcessor
    {
        public PageProcessor(BaseSiteItemsFactory siteItemsFactory)
        {
            mSiteItemsFactory = siteItemsFactory;
        }

        private BaseSiteItemsFactory mSiteItemsFactory;

        public void Run(PageNodeInfo info)
        {
            var tmpSitePage = sitePage.LoadFromFile(info.SourceName);

            if (string.IsNullOrWhiteSpace(tmpSitePage.contentPath))
            {
                tmpSitePage.contentPath = Path.Combine(Path.GetDirectoryName(info.SourceName), Path.GetFileNameWithoutExtension(info.SourceName) + ".thtml");
            }

            var sb = new StringBuilder();

            if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.mainTitle))
            {
                sb.Append(GeneralSettings.SiteSettings.mainTitle);

                if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.titlesDelimiter))
                {
                    sb.Append(GeneralSettings.SiteSettings.titlesDelimiter);
                }
            }

            if (string.IsNullOrWhiteSpace(tmpSitePage.title))
            {
                sb.Append(GeneralSettings.SiteSettings.title);
            }
            else
            {
                sb.Append(tmpSitePage.title);
            }

            tmpSitePage.title = sb.ToString().Trim();

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run info.SourceName = {info.SourceName}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run tmpSitePage.specialProcessing = {tmpSitePage.specialProcessing}");
#endif

            BasePage tmpPage = null;

            if(string.IsNullOrWhiteSpace(tmpSitePage.specialProcessing))
            {
                tmpPage = mSiteItemsFactory.CreatePage();
            }
            else
            {
                tmpPage = mSiteItemsFactory.CreatePageForSpecialProcessing(tmpSitePage.specialProcessing.Trim());
            }

            tmpPage.Title = tmpSitePage.title;

            if (!string.IsNullOrWhiteSpace(tmpSitePage.additionalMenu))
            {
                tmpPage.AdditionalMenu = menu.GetMenu(tmpSitePage.additionalMenu);
            }

            if (!string.IsNullOrWhiteSpace(tmpSitePage.description))
            {
                tmpSitePage.description = tmpSitePage.description.Trim();
            }

            tmpPage.Description = tmpSitePage.description;

            if(string.IsNullOrWhiteSpace(tmpSitePage.specialProcessing))
            {
                var tmpFileInfo = new FileInfo(tmpSitePage.contentPath);

                tmpPage.LastUpdateDate = tmpFileInfo.LastWriteTime;

                using (var tmpTextReader = new StreamReader(tmpSitePage.contentPath))
                {
                    tmpPage.Content = tmpTextReader.ReadToEnd();
                }
            }
            else
            {
                tmpPage.LastUpdateDate = DateTime.Now;
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run tmpPage.LastUpdateDate = {tmpPage.LastUpdateDate}");
#endif
            tmpPage.SourceName = info.SourceName;
            tmpPage.TargetFileName = Path.Combine(info.TargetDirName, Path.GetFileNameWithoutExtension(info.SourceName) + "." + tmpSitePage.extension);
            tmpPage.EnableMathML = tmpSitePage.enableMathML;
            tmpPage.UseMarkdown = tmpSitePage.useMarkdown;

            tmpPage.Run();
        }
    }
}
