using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatypemanSiteSupport
{
    public abstract class BasePageTemplate : BasePage
    {
        protected override void GenerateText()
        {
            AppendLine("<!DOCTYPE html>");
            AppendLine("<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>");
            AppendLine("<head>");
            AppendLine("<meta charset='utf-8' />");

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
                AppendLine("<link rel='icon' href='/favicon.png' type='image/png'>");
            }

            AppendLine("</head>");
            AppendLine("<body>");
            AppendLine("<header>");
            GenerateHeader();
            AppendLine("</header>");
            AppendLine("<nav>");
            AppendLine("</nav>");
            AppendLine("<hr>");
            AppendLine("<article>");
            GenerateArticle();
            AppendLine("<p>&nbsp;</p>");
            AppendLine("</article>");
            AppendLine("<footer>");
            AppendLine($"This page was last modified on {LastUpdateDate.ToString("dd MMMM yyyy", TargetCulture)}</br>");
            AppendLine($"&copy;&nbsp; Tolkachov Sergiy(<a href='https://github.com/metatypeman'>metatypeman</a>) 2018</br>");
            AppendLine("The text is available under the <a href='https://creativecommons.org/licenses/by-sa/3.0/'>Creative Commons Attribution-ShareAlike BY SA 3.0 Unported License</a>");
            AppendLine("</footer>");
            AppendLine("</body>");
            AppendLine("</html>");
        }

        protected virtual void GenerateArticle()
        {
//#if DEBUG
//            UseMarkdown = true;//tmp

//            AppendContent("**Hello world!**");//tmp
//#endif
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
            Append("Personal Page of Tolkachov Sergiy");
            Append("</span>");
            AppendLine("</p>");
        }
    }
}
