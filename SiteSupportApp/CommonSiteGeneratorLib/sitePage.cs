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

using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace CommonSiteGeneratorLib
{
    public class sitePage
    {
        public string extension { get; set; } = "html";
        public string contentPath { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string breadcrumbTitle { get; set; } = string.Empty;
        public bool isBreadcrumbRoot { get; set; }
        public string additionalMenu { get; set; } = null;
        public bool enableMathML { get; set; }
        public bool useMarkdown { get; set; }
        public string specialProcessing { get; set; } = string.Empty;
        public bool isReady { get; set; }
        public sitePageMicroData microdata { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(extension)} = {extension}");
            sb.AppendLine($"{nameof(contentPath)} = {contentPath}");
            sb.AppendLine($"{nameof(title)} = {title}");
            sb.AppendLine($"{nameof(breadcrumbTitle)} = {breadcrumbTitle}");
            sb.AppendLine($"{nameof(isBreadcrumbRoot)} = {isBreadcrumbRoot}");
            sb.AppendLine($"{nameof(additionalMenu)} = {additionalMenu}");
            sb.AppendLine($"{nameof(enableMathML)} = {enableMathML}");
            sb.AppendLine($"{nameof(useMarkdown)} = {useMarkdown}");
            sb.AppendLine($"{nameof(specialProcessing)} = {specialProcessing}");
            sb.AppendLine($"{nameof(isReady)} = {isReady}");
            sb.AppendLine($"{nameof(microdata)} = {microdata}");
            return sb.ToString();
        }

        public static sitePage LoadFromFile(string path)
        {
            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return new sitePage();
                }

                using (var reader = new StreamReader(tmpfile))
                {
                    return JsonConvert.DeserializeObject<sitePage>(reader.ReadToEnd());
                }
            }
        }
    }
}
