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
    public class DirProcesor
    {
        public class SiteNodeInfo
        {
            public string RelativeDirName = string.Empty;

            public string SourceDirName = string.Empty;

            public string TargetDirName = string.Empty;
        }

        public static void Run(SiteNodeInfo info)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Run");
            NLog.LogManager.GetCurrentClassLogger().Info("info.SourceDirName = {0}", info.SourceDirName);
            NLog.LogManager.GetCurrentClassLogger().Info("info.TargetDirName = {0}", info.TargetDirName);
            NLog.LogManager.GetCurrentClassLogger().Info("info.RelativeDirName = {0}", info.RelativeDirName);

            var tmpFiles = Directory.GetFiles(info.SourceDirName, "*.sp");

            foreach (var file in tmpFiles)
            {
                NLog.LogManager.GetCurrentClassLogger().Info(file);

                var tmpInfo = new PageProcessor.PageNodeInfo();

                tmpInfo.SourceName = file;
                tmpInfo.FileNameWithOutExtension = Path.GetFileNameWithoutExtension(file);
                tmpInfo.TargetDirName = info.TargetDirName;

                PageProcessor.Run(tmpInfo);
            }
        }
    }
}
