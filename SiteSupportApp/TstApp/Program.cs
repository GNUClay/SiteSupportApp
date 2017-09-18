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

using SiteGenerator;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetPath();
        }

        private static void GetPath()
        {
            foreach(DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"GetPath item.Key = {item.Key} item.Value = {item.Value}");
            }

            var sourcePath = GeneralSettings.SourcePath;

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath sourcePath = {sourcePath}");
        }
    }
}
