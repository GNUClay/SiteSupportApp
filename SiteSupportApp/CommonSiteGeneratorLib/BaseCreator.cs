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
using System.IO;

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
            NLog.LogManager.GetCurrentClassLogger().Info($"ProcessDir dir = {dir}");
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
            NLog.LogManager.GetCurrentClassLogger().Info("Begin PredictionDirProcessing");
#endif

            var context = new ContextOfPredictionDirProcessing();
            PredictionProcessingOfConcreteDir(GeneralSettings.SourcePath, context);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info("End PredictionDirProcessing");
#endif
        }

        private void PredictionProcessingOfConcreteDir(string dirName, ContextOfPredictionDirProcessing context)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Begin PredictionProcessingOfConcreteDir dirName = {dirName}");
#endif

            var tmpFilesNamesList = Directory.GetFiles(dirName, "*.sp");

            foreach (var fileName in tmpFilesNamesList)
            {
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir fileName = {fileName}");
#endif

                var newFileName = fileName.Replace(".sp", ".html");

                var relativeHref = PagesPathsHelper.PathToRelativeHref(newFileName);
                relativeHref = relativeHref.Replace(@"\sitesource", string.Empty);
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"PredictionProcessingOfConcreteDir relativeHref = {relativeHref}");
#endif
            }

            var tmpDirsList = Directory.GetDirectories(dirName);

            foreach (var subDirName in tmpDirsList)
            {
                PredictionProcessingOfConcreteDir(subDirName, context);
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"End PredictionProcessingOfConcreteDir dirName = {dirName}");
#endif
        }
    }
}
