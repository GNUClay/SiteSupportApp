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

using CommonUtils;
using Newtonsoft.Json;
using System.IO;

namespace CommonSiteGeneratorLib.SiteData
{
    public class SiteInfo
    {
        public MenuInfo Menu { get; set; }
        public string MainTitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string TitlesDelimiter { get; set; } = string.Empty;
        public bool EnableFavicon { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string RoadMapJsonPath { get; set; } = string.Empty;

        private void Init()
        {
            if(!string.IsNullOrWhiteSpace(RoadMapJsonPath))
            {
                RoadMapJsonPath = EVPath.Normalize(RoadMapJsonPath);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static SiteInfo LoadFromFile(string path)
        {
            var result = JsonConvert.DeserializeObject<SiteInfo>(File.ReadAllText(path));
            result.Init();
            return result;
        }
    }
}
