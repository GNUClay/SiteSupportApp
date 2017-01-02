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
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public System.DateTime LastUpdateDate { get; set; } = System.DateTime.Now;
        public menu AdditionalMenu { get; set; }
        public bool UseMinification { get; set; } = false;

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
            if(UseMinification)
            {
                val = val.Trim();
            }
            mResult.Append(val);
        }

        protected void AppendLine(string val)
        {
            if (UseMinification)
            {
                Append(val);
                return;
            }

            mResult.AppendLine(val);
        }

        private void GenerateText()
        {
            var tmpFormat = new System.Globalization.CultureInfo("en-GB");

            AppendLine("<!DOCTYPE html>");
            AppendLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            AppendLine("    <head>");
            AppendLine("        <meta charset=\"utf-8\" />");
            AppendLine("        <meta name='generator' content='GNUClay/SiteSupportApp'>");

            if(!string.IsNullOrWhiteSpace(Description))
            {
                Append("<meta name='description' content='");
                Append(Description);              
                AppendLine("'>");
            }

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

            AppendLine("<link rel='stylesheet' href='/semantic.min.css'>");
            AppendLine("<script src='/jquery-3.1.1.min.js'></script>");
            AppendLine("<script src='/semantic.min.js'></script>");

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
            AppendLine("    <body style='margin: 8px; font-family: sans-serif;'>");
            AppendLine("        <header>");
            GenerateMainWarning();
            GenerateHeader();
            AppendLine("        </header>");

            AppendLine("        <nav>");
            GenerateMainMenu();
            AppendLine("        </nav>");

            AppendLine("<hr>");

            if(AdditionalMenu == null)
            {
                AppendLine("        <article>");
                AppendLine(Content);
                AppendLine("        </article>");
            }
            else
            {
                AppendLine("        <div style='display: table; width: 100%;'>");
                AppendLine("            <div style='display: table-row;'>");
                AppendLine("            <div style='display: table-cell; position: relative;  vertical-align: top; width: 260px; max-width: 500px;'>");
                GenerateAdditionalMenu();
                AppendLine("        </div>");
                AppendLine("        <div style='display: table-cell; position: relative; vertical-align: top;'>");
                AppendLine("        <article>");
                AppendLine(Content);
                AppendLine("        </article>");
                AppendLine("        </div>");
                AppendLine("        </div>");
                AppendLine("        </div>");
            }

            AppendLine("<footer style='font-size: 9px;'>");
            Append("This page was last modified on ");
            Append(LastUpdateDate.ToString("dd MMMM yyyy", tmpFormat));
            //Append(", at ");
            //Append(LastUpdateDate.ToString("HH:mm", tmpFormat));
            AppendLine("</br>");

            AppendLine("&copy;&nbsp; <a href='https://github.com/metatypeman'>metatypeman</a> 2016 - 2017</br>");
            AppendLine("The text is available under the <a href='https://creativecommons.org/licenses/by-sa/3.0/'>Creative Commons Attribution-ShareAlike 3.0 Unported License</a>");
            AppendLine("</footer>");

            AppendLine("    </body>");
            AppendLine("</html>");
        }

        private void GenerateHeader()
        {         
            Append("<p>");

            if(!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.logo))
            {
                Append("<a href = '/'>");
                Append("<img src='");
                Append(GeneralSettings.SiteSettings.logo);
                Append("'>");
                Append("</a>");
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

        private void GenerateMainWarning()
        {
            AppendLine("<div style='background-color: red;'>");
            AppendLine("<i class='warning sign icon big'></i> This poject is experimental. And it is not suitable for practical use.");
            AppendLine("</div>");
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

        private void GenerateAdditionalMenu()
        {
            GenerateAdditionalMenuRunItems(AdditionalMenu.items, false);

            AppendLine("<script>$('.ui.accordion').accordion({exclusive:false});</script>");
        }

        private void GenerateAdditionalMenuRunItems(List<item> items, bool isChild)
        {
            foreach (var item in items)
            {
                if (item.items == null || item.items.Count == 0)
                {
                    AppendLine($"<ul><li><a href='{item.href}' target='_blank'>{item.label}</a></li></ul>");
                }
                else
                {
                    if(isChild)
                    {
                        AppendLine("<ul><li>");
                        AppendLine($"<p>{item.label}</p>");
                        GenerateAdditionalMenuRunItems(item.items, true);
                        AppendLine("</li></ul>");
                    }
                    else
                    {
                        AppendLine("<div class='ui accordion'>");
                        AppendLine("<div class='title'>");
                        AppendLine("<i class='dropdown icon'></i>");
                        AppendLine(item.label);
                        AppendLine("</div>");
                        AppendLine("<div class ='content'>");
                        GenerateAdditionalMenuRunItems(item.items, true);
                        AppendLine("</div>");
                        AppendLine("</div>");
                    }
                }
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
