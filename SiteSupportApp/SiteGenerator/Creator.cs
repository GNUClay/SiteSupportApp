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
    public class Creator
    {
        public Creator()
        {
        }

        public void Run()
        {
            ClearDir();
            ProcessDir(GeneralSettings.SourcePath);
        }

        private void ClearDir()
        {
            var tmpDirs = Directory.GetDirectories(GeneralSettings.DestPath);

            foreach (var subDir in tmpDirs)
            {
                var tmpDirInfo = new DirectoryInfo(subDir);

                if (tmpDirInfo.Name == GeneralSettings.IgnoreDestDir)
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
            var tmpInfo = new DirProcesor.SiteNodeInfo();

            tmpInfo.SourceDirName = dir;
            tmpInfo.RelativeDirName = dir.Substring(GeneralSettings.SourcePath.Length);
            tmpInfo.TargetDirName = Path.Combine(GeneralSettings.DestPath, tmpInfo.RelativeDirName);

            DirProcesor.Run(tmpInfo);

            var tmpDirs = Directory.GetDirectories(dir);

            foreach (var subDir in tmpDirs)
            {
                ProcessDir(subDir);
            }
        }
    }
}
