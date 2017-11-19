using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator
{
    public class BaseTargetPage
    {
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public System.DateTime LastUpdateDate { get; set; } = System.DateTime.Now;
        public menu AdditionalMenu { get; set; }
        public bool UseMinification { get; set; } = false;

        private StringBuilder mResult = null;

        public virtual void Run()
        {
            mResult = new StringBuilder();
            GenerateText();
        }

        public virtual void Run(string targetFileName)
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
            if (UseMinification)
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
            AppendLine("<head>");
            AppendLine("<meta charset=\"utf-8\" />");
            AppendLine("<meta name='generator' content='GNUClay/SiteSupportApp'>");

            if (!string.IsNullOrWhiteSpace(Description))
            {
                Append("<meta name='description' content='");
                Append(Description);
                AppendLine("'>");
            }

            if (!string.IsNullOrWhiteSpace(Title))
            {
                Append("<title>");
                Append(Title);
                AppendLine("</title>");
            }

            if (GeneralSettings.SiteSettings.enabledFavicon)
            {
                //AppendLine("<link rel='shortcut icon' href='/favicon.ico' type='image/x-icon'>");
                AppendLine("<link rel='icon' href='/favicon.png' type='image/png'>");
            }

            AppendLine("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css' integrity='sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u' crossorigin='anonymous'>");
            AppendLine("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css' integrity='sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp' crossorigin='anonymous'>");
            AppendLine("<link rel='stylesheet' href='/site.css'>");

            if (AdditionalMenu != null)
            {
                AppendLine("<link rel='stylesheet' href='/gnu-clay-menu.css'>");
            }

            AppendLine("<script src='https://code.jquery.com/jquery-3.2.1.js'></script>");
            AppendLine("<script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js' integrity='sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa' crossorigin='anonymous'></script>");
            AppendLine("<script src='https://use.fontawesome.com/9ecadafb0a.js'></script>");

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

            AppendLine("</head>");
            AppendLine("<body>");
            AppendLine("<div class='container main-container'>");
            AppendLine("<div class='row justify-content-center'>");
            AppendLine("<div class='col col-md-10'>");
            AppendLine("<header>");
            GenerateMainWarning();
            GenerateHeader();
            AppendLine("</header>");
            AppendLine("<nav>");
            GenerateMainMenu();
            AppendLine("</nav>");
            AppendLine("<hr>");
            AppendLine("</div>");
            AppendLine("</div>");

            AppendLine("<div class='row justify-content-center'>");
            AppendLine("<div class='col col-md-10'>");

            if (AdditionalMenu == null)
            {
                GenerateArticle();
            }
            else
            {
                AppendLine("<div class='container-fluid'>");
                AppendLine("<div class='row'>");
                AppendLine("<div class='col col-md-3 my-menu-col'>");
                GenerateAdditionalMenu();
                AppendLine("</div>");
                AppendLine("<div class='col col-md-9'>");
                GenerateArticle();
                AppendLine("</div>");
                AppendLine("</div>");
                AppendLine("</div>");
            }

            AppendLine("</div>");
            AppendLine("</div>");
            AppendLine("</div>");

            AppendLine("<footer class='container'>");
            AppendLine("<div class='row justify-content-center'>");
            AppendLine("<div class='col col-md-10'>");
            AppendLine($"This page was last modified on {LastUpdateDate.ToString("dd MMMM yyyy", tmpFormat)}</br>");
            //Append(", at ");
            //Append(LastUpdateDate.ToString("HH:mm", tmpFormat));

            AppendLine($"&copy;&nbsp; <a href='https://github.com/metatypeman'>metatypeman</a> 2016 - {DateTime.Today.Year}</br>");
            AppendLine("The text is available under the <a href='https://creativecommons.org/licenses/by-sa/3.0/'>Creative Commons Attribution-ShareAlike 3.0 Unported License</a>");
            AppendLine("</div>");
            AppendLine("</div>");
            AppendLine("</footer>");

            AppendLine("</body>");
            AppendLine("</html>");
        }

        protected virtual void GenerateArticle()
        {
        }

        private void GenerateHeader()
        {
            Append("<p>");

            if (!string.IsNullOrWhiteSpace(GeneralSettings.SiteSettings.logo))
            {
                Append("<a href = '/'>");
                Append("<img src='");
                Append(GeneralSettings.SiteSettings.logo);
                Append("' style='margin-top: -12px;'>");
                Append("</a>");
                Append("&nbsp;");
            }

            Append("<span style='font-size: 30px; font-weight: bold;'>");
            Append("GNU Clay");
            Append("</span>");
            Append("&nbsp;");
            Append("<span>");
            Append("The small and simple game AI");
            Append("</span>");
            AppendLine("</p>");
        }

        private void GenerateMainWarning()
        {
            AppendLine("<div class='not-suitable-danger'>");
            AppendLine("<i class='fa fa-exclamation-triangle' aria-hidden='true' style='font-size:20px;'></i> <span class='not-suitable-danger-text'>This project is experimental. And it is not suitable for practical using.</span>");
            AppendLine("</div>");
        }

        private void GenerateMainMenu()
        {
            var tmpItems = new List<string>();

            foreach (var item in GeneralSettings.SiteSettings.menu.items)
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
        }

        private int mN = 0;

        private void GenerateAdditionalMenuRunItems(List<item> items, bool isChild)
        {
            foreach (var item in items)
            {
                if (item.items == null || item.items.Count == 0)
                {
                    if (isChild)
                    {
                        AppendLine($"<ul class='my-second-menu-item'><li><a href='{item.href}' target='_blank'>{item.label}</a></li></ul>");
                    }
                    else
                    {
                        AppendLine($"<p class='my-root-menu-item'><a href='{item.href}' target='_blank'>{item.label}</a></p>");
                    }
                }
                else
                {
                    if (isChild)
                    {
                        AppendLine("<ul class='my-second-menu-item'><li>");
                        AppendLine($"<p class='my-second-menu-label'>{item.label}</p>");
                        GenerateAdditionalMenuRunItems(item.items, true);
                        AppendLine("</li></ul>");
                    }
                    else
                    {
                        mN++;

                        AppendLine($"<div class='panel-group myslim-panel-group' id='accordion_{mN}' role='tablist' aria-multiselectable='true'>");
                        AppendLine("<div class='panel panel-default' style='margin-bottom: 5px;'>");
                        AppendLine($"<div class='panel-heading myslim-panel-heading' role='tab' id='headingOne_{mN}'>");
                        AppendLine($"<a class='myslim-panel-button' role='button' data-toggle='collapse' data-parent='#accordion_{mN}' href='#collapseOne_{mN}' aria-expanded='false' aria-controls='collapseOne_{mN}'>");
                        AppendLine($"&#9660;&nbsp;<span style='font-weight: bold;'>{item.label}</span>");
                        AppendLine("</a>");
                        AppendLine("</div>");
                        AppendLine($"<div id='collapseOne_{mN}' class='panel-collapse collapse' role='tabpanel' aria-labelledby='headingOne_{mN}'>");
                        AppendLine("<div class='panel-body myslim-panel-body'>");
                        GenerateAdditionalMenuRunItems(item.items, true);
                        AppendLine("</div>");
                        AppendLine("</div>");
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

                var result = mResult.ToString();

                if(UseMinification)
                {
                    //result = result.Replace(Environment.NewLine, string.Empty);
                }
               
                return result.Trim();
            }
        }
    }
}
