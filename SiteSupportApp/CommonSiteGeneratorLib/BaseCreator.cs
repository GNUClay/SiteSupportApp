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

using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonSiteGeneratorLib
{
    public abstract class BaseCreator
    {
        protected BaseCreator()
        {
        }

        protected virtual BaseSiteItemsFactory GetsiteItemsFactory()
        {
            var factory = new BaseSiteItemsFactory();
            return factory;
        }

        private BaseSiteItemsFactory mSiteItemsFactory;
        private BaseDirProcesor mDirProcesor;

        public void Run()
        {
            mSiteItemsFactory = GetsiteItemsFactory();
            mDirProcesor = mSiteItemsFactory.CreateDirProcessor();
            ClearDir();
            PredictionDirProcessing();
            ProcessDir(GeneralSettings.SourcePath);
        }

        private void ClearDir()
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"ClearDir GeneralSettings.DestPath = {GeneralSettings.DestPath}");
#endif

            var tmpDirs = Directory.GetDirectories(GeneralSettings.DestPath);

            foreach (var subDir in tmpDirs)
            {
                var tmpDirInfo = new DirectoryInfo(subDir);

                if (tmpDirInfo.Name == GeneralSettings.IgnoreDestDir)
                {
                    continue;
                }

                if (tmpDirInfo.Name == GeneralSettings.IgnoreGitDir)
                {
                    continue;
                }

                tmpDirInfo.Delete(true);
            }

            var tmpFiles = Directory.GetFiles(GeneralSettings.DestPath);

            foreach (var file in tmpFiles)
            {
                File.Delete(file);
            }
        }

        private void ProcessDir(string dir)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"ProcessDir dir = {dir}");
#endif

            var tmpInfo = new SiteNodeInfo();

            tmpInfo.SourceDirName = dir;
            tmpInfo.RelativeDirName = dir.Substring(GeneralSettings.SourcePath.Length);
            tmpInfo.TargetDirName = Path.Combine(GeneralSettings.DestPath, tmpInfo.RelativeDirName);

            mDirProcesor.Run(tmpInfo);

            var tmpDirs = Directory.GetDirectories(dir);

            foreach (var subDir in tmpDirs)
            {
                ProcessDir(subDir);
            }
        }

        private void PredictionDirProcessing()
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info("Begin PredictionDirProcessing");
#endif

            var context = new ContextOfPredictionDirProcessing();
            PredictionProcessingOfConcreteDir(GeneralSettings.SourcePath, null, context);
            GenerateSiteMap(context.Pages);
            mSiteItemsFactory.SetBreadcrumbsPageNodes(context.Pages);
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info("End PredictionDirProcessing");
#endif
        }

        private void PredictionProcessingOfConcreteDir(string dirName, BreadcrumbsPageNode parent, ContextOfPredictionDirProcessing context)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Begin PredictionProcessingOfConcreteDir dirName = {dirName}");
#endif

            var tmpFilesNamesList = Directory.GetFiles(dirName, "*.sp");

            var indexFileName = tmpFilesNamesList.ToList().FirstOrDefault(p => p.EndsWith("index.sp"));

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Begin PredictionProcessingOfConcreteDir indexFileName = {indexFileName}");
#endif

            BreadcrumbsPageNode newParent = null;

            if (!string.IsNullOrWhiteSpace(indexFileName))
            {
                newParent = PredictionProcessingOfConcreteFile(indexFileName, true, parent, context);
            }
            
            foreach (var fileName in tmpFilesNamesList)
            {
                if (fileName.EndsWith("index.sp"))
                {
                    continue;
                }

                PredictionProcessingOfConcreteFile(fileName, false, newParent, context);
            }

            var tmpDirsList = Directory.GetDirectories(dirName);

            foreach (var subDirName in tmpDirsList)
            {
                PredictionProcessingOfConcreteDir(subDirName, newParent, context);
            }

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"End PredictionProcessingOfConcreteDir dirName = {dirName}");
#endif
        }

        private BreadcrumbsPageNode PredictionProcessingOfConcreteFile(string fileName, bool isIndex, BreadcrumbsPageNode parent, ContextOfPredictionDirProcessing context)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteFile fileName = {fileName} isIndex = {isIndex}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir parent = {parent}");
#endif

            var newFileName = fileName.Replace(".sp", ".html");

            var relativeHref = PagesPathsHelper.PathToRelativeHref(newFileName);
            relativeHref = relativeHref.Replace(@"\sitesource", string.Empty);
            var absoluteHref = PagesPathsHelper.RelativeHrefToAbsolute(relativeHref);
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir relativeHref = {relativeHref}");
#endif

            var tmpSitePage = sitePage.LoadFromFile(fileName);

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir tmpSitePage = {tmpSitePage}");
#endif

            if(!tmpSitePage.isReady)
            {
                return null;
            }

            var result = new BreadcrumbsPageNode();
            context.Pages.Add(result);
            result.Parent = parent;
            result.IsIndex = isIndex;
            result.Path = fileName;
            result.RelativeHref = relativeHref;
            result.AbsoluteHref = absoluteHref;
            result.Title = tmpSitePage.breadcrumbTitle;

            if(string.IsNullOrWhiteSpace(result.Title))
            {
                result.Title = tmpSitePage.title;
            }
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir result = {result}");
#endif

            return result;
        }

        private void GenerateSiteMap(List<BreadcrumbsPageNode> pagesList)
        {
            var sb = new StringBuilder(); 
            sb.AppendLine("<?xml version='1.0' encoding='UTF-8'?>");
            sb.AppendLine("<urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>");
            foreach(var page in pagesList)
            {
                //var uriBuilder = new UriBuilder();
                //uriBuilder.Scheme = "https";
                //uriBuilder.Host = GeneralSettings.SiteName;
                //uriBuilder.Path = page.RelativeHref;

                //var lastMod = DateTime.Now;

                sb.AppendLine("<url>");
                sb.AppendLine($"<loc>{page.AbsoluteHref}</loc>");
                //sb.AppendLine($"<lastmod>{lastMod.ToString("yyyy-MM-dd")}</lastmod>");
                //sb.AppendLine("<changefreq>always</changefreq>");
                //sb.AppendLine("<priority>0.8</priority>");
                sb.AppendLine("</url>");
            }
            sb.AppendLine("</urlset>");

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir sb.ToString() = {sb.ToString()}");
#endif

            var newPath = Path.Combine(GeneralSettings.DestPath, "sitemap.xml");

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir newPath = {newPath}");
#endif

            using (var tmpTextWriter = new StreamWriter(newPath, false, new UTF8Encoding(true)))
            {
                tmpTextWriter.Write(sb.ToString());
                tmpTextWriter.Flush();
            }
        }
    }
}
