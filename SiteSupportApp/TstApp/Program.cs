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
            TSTSaveSiteInfo();
            //TSTConvertingDocBookToHtml();
        }

        private static void TSTSaveSiteInfo()
        {
            var path = @"c:\Users\Sergey\Documents\neo.xml";

            var tmpObj = site.LoadFromFile(path);

            var a = 0;

            /*var tmpObj = new site();

            tmpObj.menu = new menu();

            var tmpItem = new item();

            tmpItem.href = "https://github.com";
            tmpItem.label = "Git Hub";

            tmpObj.menu.items.Add(tmpItem);

            using (var tmpfile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                var tmpSerializer = new XmlSerializer(typeof(site));

                var tmpWriter = XmlWriter.Create(tmpfile);

                tmpSerializer.Serialize(tmpWriter, tmpObj);

                tmpWriter.Flush();

                tmpfile.Flush();
            }

            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return;
                }

                var tmpSerializer = new XmlSerializer(typeof(site), new Type[] {typeof(menu), typeof(item)});

                var tmpReader = XmlReader.Create(tmpfile);

                var tmpSite = (site)tmpSerializer.Deserialize(tmpReader);

                var a = 0;
            }*/
        }

        private static void TSTConvertingDocBookToHtml()
        {
            using(var tmpReader = new StreamReader("ArticleExample.xml"))
            {
                var tmpProcessor = new SiteGenerator.DocBookProcessor.DbkToHtml.SimpleProcessor(tmpReader);

                tmpProcessor.Run();

                var tmpStringWriter = new StringWriter();

                tmpProcessor.ResultDocument.Save(tmpStringWriter);

                NLog.LogManager.GetCurrentClassLogger().Info(tmpStringWriter.ToString());

                var tmpStr = tmpStringWriter.ToString();

                tmpStr = tmpStr.Replace("<t>", string.Empty).Replace("</t>", string.Empty).Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty).Trim();

                NLog.LogManager.GetCurrentClassLogger().Info(tmpStr);

                var tmpSb = new StringBuilder();

                tmpSb.Append("<!DOCTYPE HTML>");
                tmpSb.Append("<html>");
                tmpSb.Append("<head>");
                tmpSb.Append("</head>");
                tmpSb.Append("<body>");
                tmpSb.Append(tmpStr);
                tmpSb.Append("</body>");
                tmpSb.Append("</html>");

                File.WriteAllText("example.html", tmpSb.ToString(), Encoding.UTF8);
            }
        }
    }
}
