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

namespace CommonSiteGeneratorLib
{
    public class BaseDirProcesor
    {
        public BaseDirProcesor(BaseSiteItemsFactory siteItemsFactory)
        {
            mPageProcessor = new PageProcessor(siteItemsFactory);
        }

        private PageProcessor mPageProcessor;

        public virtual void Run(SiteNodeInfo info)
        {
            if (!Directory.Exists(info.TargetDirName))
            {
                Directory.CreateDirectory(info.TargetDirName);
            }

            var tmpFiles = Directory.GetFiles(info.SourceDirName, "*.sp");

            foreach (var file in tmpFiles)
            {
                var tmpInfo = new PageNodeInfo();

                tmpInfo.SourceName = file;
                tmpInfo.FileNameWithOutExtension = Path.GetFileNameWithoutExtension(file);
                tmpInfo.TargetDirName = info.TargetDirName;

                mPageProcessor.Run(tmpInfo);
            }

            tmpFiles = Directory.GetFiles(info.SourceDirName);

            foreach (var file in tmpFiles)
            {
                var tmpFileExtension = Path.GetExtension(file);

                if (tmpFileExtension == ".sp")
                {
                    continue;
                }

                if (tmpFileExtension == ".thtml")
                {
                    continue;
                }

                if (tmpFileExtension == ".site")
                {
                    continue;
                }

                if (tmpFileExtension == ".menu")
                {
                    continue;
                }

                if (tmpFileExtension == ".md")
                {
                    continue;
                }

                if (tmpFileExtension == ".dia")
                {
                    continue;
                }

                if (tmpFileExtension == ".dia~")
                {
                    continue;
                }

                if (tmpFileExtension == ".json")
                {
                    continue;
                }

                var tmpTargetFileName = Path.Combine(info.TargetDirName, Path.GetFileName(file));

                File.Copy(file, tmpTargetFileName, true);
            }
        }
    }
}
