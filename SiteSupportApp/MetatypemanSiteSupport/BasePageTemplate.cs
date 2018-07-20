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
        protected BasePageTemplate(BaseSiteItemsFactory factory)
            : base(factory)
        {
        }

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
            AppendLine("<link rel='stylesheet' href='/site.css'>");
            AppendLine("</head>");
            AppendLine("<body>");
            AppendLine("<header>");
            GenerateHeader();
            AppendLine("</header>");
            AppendLine("<nav>");
            GenerateMainMenu();
            AppendLine("</nav>");
            AppendLine("<hr>");
            AppendLine("<nav class='bread-crumb-nav'>");
            GenerateBreadcrumbs();
            AppendLine("</nav>");
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

        private void GenerateMainMenu()
        {
            var tmpItems = new List<string>();

            foreach (var item in GeneralSettings.SiteSettings.menu.items)
            {
                var tmpSb = new StringBuilder();

                tmpSb.Append("<a href ='");
                tmpSb.Append(item.href);
                tmpSb.Append("'>");
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

        private class BreadcrumbInThePage
        {
            public string Title { get; set; }
            public string Href { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{nameof(Title)} = {Title}");
                sb.AppendLine($"{nameof(Href)} = {Href}");
                return sb.ToString();
            }
        }

        private void GenerateBreadcrumbs()
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs SourceName = {SourceName}");
#endif
            var breadcrumbsItem = SiteItemsFactory.GetBreadcrumbsPageNode(SourceName);

            var isFirst = true;

            var itemsList = new List<BreadcrumbInThePage>();

            do
            {
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs isFirst = {isFirst} breadcrumbsItem = {breadcrumbsItem}");
#endif

                var item = new BreadcrumbInThePage();
                item.Title = breadcrumbsItem.Title;

                if(isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    item.Href = breadcrumbsItem.RelativeHref;
                }

                itemsList.Add(item);
            }
            while ((breadcrumbsItem = breadcrumbsItem.Parent) != null);

            itemsList.Reverse();

            var n = 0;

            foreach(var item in itemsList)
            {
#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs item = {item}");
#endif
                n++;
                Append("<a");
                if(!string.IsNullOrWhiteSpace(item.Href))
                {
                    Append($" href = '{item.Href}'");
                }
                Append(">");
                Append(item.Title);
                Append("</a>");

                if(n < itemsList.Count)
                {
                    Append("&nbsp;/&nbsp;");
                }
            }
        }
    }
}
