﻿/* SiteSupportApp supports generating the web site <http://gnuclay.github.io>
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
using System.Xml;
using System.Xml.Serialization;
namespace SiteGenerator
{
    public class sitePage
    {
        public string extension = "html";
        public string contentPath = string.Empty;
        public string title = string.Empty;

        public static sitePage LoadFromFile(string path)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("LoadFromFile");
            NLog.LogManager.GetCurrentClassLogger().Info("path = {0}", path);

            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return new sitePage();
                }

                var tmpSerializer = new XmlSerializer(typeof(sitePage));

                var tmpReader = XmlReader.Create(tmpfile);

                return (sitePage)tmpSerializer.Deserialize(tmpReader);
            }
        }
    }
}
