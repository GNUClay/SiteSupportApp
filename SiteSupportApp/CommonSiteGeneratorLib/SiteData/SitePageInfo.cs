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

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonSiteGeneratorLib.SiteData
{
    public class SitePageInfo
    {
        public string Extension { get; set; } = "html";
        public string ContentPath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string BreadcrumbTitle { get; set; } = string.Empty;
        public bool IsBreadcrumbRoot { get; set; }
        public string AdditionalMenu { get; set; } = null;
        public bool EnableMathML { get; set; }
        public bool UseMarkdown { get; set; }
        public string SpecialProcessing { get; set; } = string.Empty;
        public bool IsReady { get; set; }
        public SitePageMicroDataInfo Microdata { get; set; }
        public List<PagePluginInfo> PluginsPipeline { get; set; } = new List<PagePluginInfo>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static SitePageInfo LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<SitePageInfo>(File.ReadAllText(path));
        }
    }
}
