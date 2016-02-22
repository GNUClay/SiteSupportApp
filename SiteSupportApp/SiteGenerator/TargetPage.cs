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
using System.Text;
using System.Collections.Generic;

namespace SiteGenerator
{
    public class TargetPage
    {
        private string mTitle = string.Empty;

        public string Title
        {
            get
            {
                return mTitle;
            }

            set
            {
                mTitle = value;
            }
        }

        private string mContent = string.Empty;

        public string Content
        {
            get
            {
                return mContent;
            }

            set
            {
                mContent = value;
            }
        }

        private StringBuilder mResult = null;

        public void Run()
        {
            mResult = new StringBuilder();

            GenerateText();
        }

        public void Run(string targetFileName)
        {
            Run();

            using (var tmpTextWriter = new StreamWriter(targetFileName))
            {
                tmpTextWriter.Write(Result);

                tmpTextWriter.Flush();
            }
        }

        protected void Append(string val)
        {
            mResult.Append(val);
        }

        protected void AppendLine(string val)
        {
            mResult.AppendLine(val);
        }

        private void GenerateText()
        {
            AppendLine("<!DOCTYPE html>");
            AppendLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            AppendLine("    <head>");
            AppendLine("        <meta charset=\"utf-8\" />");
            AppendLine("        <meta name='generator' content='GNUClay/SiteSupportApp'>");

            if (!string.IsNullOrWhiteSpace(Title))
            {
                Append("        <title>");
                Append(Title);
                AppendLine("</title>");
            }

            if (GeneralSettings.SiteSettings.enabledFavicon)
            {
                //AppendLine("<link rel='shortcut icon' href='/favicon.ico' type='image/x-icon'>");
                AppendLine("<link rel='icon' href='/favicon.png' type='image/png'>");
            }

            AppendLine("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css'>");

            var tmpGAScript = new StringBuilder();

            tmpGAScript.Append("<script>");
            tmpGAScript.Append("(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){");
            tmpGAScript.Append("(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),");
            tmpGAScript.Append("m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)");
            tmpGAScript.Append("})(window,document,'script','//www.google-analytics.com/analytics.js','ga');");
            tmpGAScript.Append("ga('create', 'UA-73880715-1', 'auto');");
            tmpGAScript.Append("ga('send', 'pageview');");  
            tmpGAScript.Append("</script>");

            AppendLine(tmpGAScript.ToString());

            AppendLine("    </head>");
            AppendLine("    <body>");
            AppendLine("        <header>");
            GenerateHeader();
            AppendLine("        </header>");

            AppendLine("        <nav>");
            GenerateMainMenu();
            AppendLine("        </nav>");

            AppendLine("<hr>");

            AppendLine("        <article>");

            AppendLine(Content);

            AppendLine("        </article>");

            AppendLine("    </body>");
            AppendLine("</html>");
        }

        private void GenerateHeader()
        {
            Append("<p>");

            if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.logo))
            {
                Append("<img src='");
                Append(GeneralSettings.SiteSettings.logo);
                Append("'>");
                Append("&nbsp;");
            }

            Append("<span style='font-size: 30px; font-weight: bold;'>");
            Append("GNU Clay");
            Append("</span>");
            Append("&nbsp;");
            Append("<span>");
            Append("The small simple AI");
            Append("</span>");
            AppendLine("</p>");
        }

        private void GenerateMainMenu()
        {
            var tmpItems = new List<string>();

            foreach(var item in GeneralSettings.SiteSettings.menu.items)
            {
                var tmpSb = new StringBuilder();

                tmpSb.Append("<a href ='");
                tmpSb.Append(item.href);
                tmpSb.Append("' target='_blank'>");
                tmpSb.Append(item.label);
                tmpSb.Append("</a>");

                tmpItems.Add(tmpSb.ToString());
                tmpItems.Add("&nbsp;|&nbsp;");
            }

            tmpItems.RemoveAt(tmpItems.Count - 1);

            foreach (var item in tmpItems)
            {
                Append(item);
            }
        }

        public string Result
        {
            get
            {
                if (mResult == null)
                {
                    return string.Empty;
                }

                return mResult.ToString();
            }
        }
    }
}
