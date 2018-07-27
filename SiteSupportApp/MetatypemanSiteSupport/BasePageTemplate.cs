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

            AppendLine("<meta property='og:type' content='article' />");

            if (!string.IsNullOrWhiteSpace(MicrodataTitle))
            {
                AppendLine($"<meta property='og:title' content='{MicrodataTitle}' />");
                //AppendLine($"<meta itemprop='name' content='{MicrodataTitle}' />");
            }

            if (!string.IsNullOrWhiteSpace(ImageUrl))
            {
                AppendLine($"<meta property='og:image' content='{PagesPathsHelper.RelativeHrefToAbsolute(ImageUrl)}' />");
                //AppendLine($"<link rel='\"image_src\" href=\"{PagesPathsHelper.RelativeHrefToAbsolute(ImageUrl)}\" />");
                //AppendLine("<meta property='og:image:type' content='image/png'>");
                //AppendLine("<meta property='og:image:width' content='300'>");
                //AppendLine("<meta property='og:image:height' content='300'>");
                if (!string.IsNullOrWhiteSpace(ImageAlt))
                {
                    //AppendLine($"<meta property='og:image:alt' content='{ImageAlt}' />");
                }               
            }

            AppendLine($"<meta property='og:url' content='{AbsoluteHref}' />");

            if (!string.IsNullOrWhiteSpace(Title))
            {
                Append("<title>");
                Append(Title);
                AppendLine("</title>");
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                AppendLine($"<meta name='description' content='{Description}'>");
                AppendLine($"<meta itemprop='og:description' content='{Description}' />");
            }

            if (GeneralSettings.SiteSettings.enabledFavicon)
            {
                AppendLine($"<link rel='icon' href='{PagesPathsHelper.RelativeHrefToAbsolute("/favicon.png")}' type='image/png'>");
            }
            AppendLine($"<link href='{PagesPathsHelper.RelativeHrefToAbsolute("/fontawesome-free-5.2.0-web/css/all.css")}' rel='stylesheet'>");///css/all.css
            AppendLine($"<link rel='stylesheet' href='{PagesPathsHelper.RelativeHrefToAbsolute("/site.css")}'>");
            AppendLine("</head>");
            AppendLine("<body>");
            AppendLine("<header>");
            GenerateHeader();
            AppendLine("</header>");
            AppendLine("<nav>");
            GenerateMainMenu();
            AppendLine("</nav>");
            AppendLine("<hr style='border-bottom-color: #e2e2e2;'>");
            AppendLine("<nav class='bread-crumb-nav'>");
            GenerateBreadcrumbs();
            AppendLine("</nav>");
            AppendLine("<article>"); 
            GenerateArticle();
            AppendLine("<p>&nbsp;</p>");
            AppendLine("</article>");
            AppendLine("<footer>");
            AppendLine($"This page was last modified on {LastUpdateDate.ToString("dd MMMM yyyy", TargetCulture)}</br>");
            AppendLine($"&copy;&nbsp; Tolkachov Sergiy 2018</br>");
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
                Append($"<a href = 'https://{GeneralSettings.SiteName}'>");
                Append("<img src='");
                Append(PagesPathsHelper.RelativeHrefToAbsolute(GeneralSettings.SiteSettings.logo));
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
                tmpSb.Append(PagesPathsHelper.RelativeHrefToAbsolute(item.href));
                tmpSb.Append("' class='main-menu-link'>");
                tmpSb.Append(item.label);
                tmpSb.Append("</a>");

                tmpItems.Add(tmpSb.ToString());
                tmpItems.Add("&nbsp;<span style='color: gray;'>|<span>&nbsp;");
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
            //NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs SourceName = {SourceName}");
#endif
            var breadcrumbsItem = SiteItemsFactory.GetBreadcrumbsPageNode(SourceName);

            var isFirst = true;

            var itemsList = new List<BreadcrumbInThePage>();

            do
            {
#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs isFirst = {isFirst} breadcrumbsItem = {breadcrumbsItem}");
#endif

                var item = new BreadcrumbInThePage();
                item.Title = breadcrumbsItem.Title;

                if(isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    item.Href = breadcrumbsItem.AbsoluteHref;
                }

                itemsList.Add(item);
            }
            while ((breadcrumbsItem = breadcrumbsItem.Parent) != null);

            itemsList.Reverse();

            var n = 0;

            foreach(var item in itemsList)
            {
#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"GenerateBreadcrumbs item = {item}");
#endif
                n++;
                Append("<a");
                if(!string.IsNullOrWhiteSpace(item.Href))
                {
                    Append($" href = '{item.Href}'");
                }
                Append(" style='color: #C0C0C0;'>");
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
