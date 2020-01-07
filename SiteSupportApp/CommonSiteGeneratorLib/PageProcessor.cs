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

using CommonSiteGeneratorLib.SiteData;
using System;
using System.IO;
using System.Linq;
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
            var tmpSitePageInfo = SitePageInfo.LoadFromFile(info.SourceName);

            if(!tmpSitePageInfo.IsReady)
            {
                return;
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run tmpSitePage = {tmpSitePageInfo}");
#endif

            if (string.IsNullOrWhiteSpace(tmpSitePageInfo.ContentPath))
            {
                var tmpExtension = string.Empty;

                if(tmpSitePageInfo.UseMarkdown)
                {
                    tmpExtension = ".md";
                }
                else
                {
                    tmpExtension = ".thtml";
                }

                tmpSitePageInfo.ContentPath = Path.Combine(Path.GetDirectoryName(info.SourceName), Path.GetFileNameWithoutExtension(info.SourceName) + tmpExtension);
            }

            var sb = new StringBuilder();

            if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.MainTitle))
            {
                sb.Append(GeneralSettings.SiteSettings.MainTitle);

                if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.TitlesDelimiter))
                {
                    sb.Append(GeneralSettings.SiteSettings.TitlesDelimiter);
                }
            }

            if (string.IsNullOrWhiteSpace(tmpSitePageInfo.Title))
            {
                sb.Append(GeneralSettings.SiteSettings.Title);
            }
            else
            {
                sb.Append(tmpSitePageInfo.Title);
            }

            tmpSitePageInfo.Title = sb.ToString().Trim();

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run info.SourceName = {info.SourceName}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run tmpSitePage.specialProcessing = {tmpSitePage.specialProcessing}");
#endif

            BasePage tmpPage = null;

            if(string.IsNullOrWhiteSpace(tmpSitePageInfo.SpecialProcessing))
            {
                tmpPage = mSiteItemsFactory.CreatePage();
            }
            else
            {
                tmpPage = mSiteItemsFactory.CreatePageForSpecialProcessing(tmpSitePageInfo.SpecialProcessing.Trim());
            }

            tmpPage.Title = tmpSitePageInfo.Title;

            if (!string.IsNullOrWhiteSpace(tmpSitePageInfo.AdditionalMenu))
            {
                tmpPage.AdditionalMenu = MenuInfo.GetMenu(tmpSitePageInfo.AdditionalMenu);
            }

            var sitePageMicroData = tmpSitePageInfo.Microdata;

            if (sitePageMicroData != null)
            {
                if (!string.IsNullOrWhiteSpace(sitePageMicroData.Description))
                {
                    sitePageMicroData.Description = sitePageMicroData.Description.Trim();
                }

                tmpPage.Description = sitePageMicroData.Description;

                if(string.IsNullOrWhiteSpace(sitePageMicroData.Title))
                {
                    sitePageMicroData.Title = tmpSitePageInfo.Title;
                }
                else
                {
                    sitePageMicroData.Title = sitePageMicroData.Title.Trim();
                }

                tmpPage.MicrodataTitle = sitePageMicroData.Title;

                if (!string.IsNullOrWhiteSpace(sitePageMicroData.ImageUrl))
                {
                    sitePageMicroData.ImageUrl = sitePageMicroData.ImageUrl.Trim();
                }

                tmpPage.ImageUrl = sitePageMicroData.ImageUrl;

                if (!string.IsNullOrWhiteSpace(sitePageMicroData.ImageAlt))
                {
                    sitePageMicroData.ImageAlt = sitePageMicroData.ImageAlt.Trim();
                }

                tmpPage.ImageAlt = sitePageMicroData.ImageAlt;

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageMicroData.title = {sitePageMicroData.Title}");
                NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageMicroData.imageUrl = {sitePageMicroData.ImageUrl}");
                NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageMicroData.imageAlt = {sitePageMicroData.ImageAlt}");
#endif
            }

            if (string.IsNullOrWhiteSpace(tmpSitePageInfo.SpecialProcessing))
            {
                var tmpFileInfo = new FileInfo(tmpSitePageInfo.ContentPath);

                tmpPage.LastUpdateDate = tmpFileInfo.LastWriteTime;

                if(tmpSitePageInfo.PluginsPipeline.Any())
                {
                    RunContentPreprocessingPipeline(tmpPage, tmpSitePageInfo, tmpSitePageInfo.ContentPath);
                }
                else
                {
                    using (var tmpTextReader = new StreamReader(tmpSitePageInfo.ContentPath))
                    {
                        tmpPage.Content = tmpTextReader.ReadToEnd();
                    }
                }
            }
            else
            {
                tmpPage.LastUpdateDate = DateTime.Now;
            }

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run tmpPage.LastUpdateDate = {tmpPage.LastUpdateDate}");
#endif
            tmpPage.SourceName = info.SourceName;
            tmpPage.TargetFileName = Path.Combine(info.TargetDirName, Path.GetFileNameWithoutExtension(info.SourceName) + "." + tmpSitePageInfo.Extension);
            tmpPage.EnableMathML = tmpSitePageInfo.EnableMathML;
            tmpPage.UseMarkdown = tmpSitePageInfo.UseMarkdown;

            tmpPage.Run();
        }

        private void RunContentPreprocessingPipeline(BasePage page, SitePageInfo sitePageInfo, string sourceFileName)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sourceFileName = {sourceFileName}");
#endif

            var targetSourceFileName = sourceFileName;

            foreach(var pluginInfo in sitePageInfo.PluginsPipeline)
            {
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"Run pluginInfo = {pluginInfo}");
                NLog.LogManager.GetCurrentClassLogger().Info($"Run targetSourceFileName = {targetSourceFileName}");
#endif

                var pluginInstance = mSiteItemsFactory.GetPipelineItem(pluginInfo.Name);
                targetSourceFileName = pluginInstance.Run(targetSourceFileName, sitePageInfo, pluginInfo);
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run targetSourceFileName = {targetSourceFileName}");
#endif

            var content = File.ReadAllText(targetSourceFileName);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run content = {content}");
#endif

            page.Content = content;
        }
    }
}
