using CommonSiteGeneratorLib.SiteData;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.ShortTagsPreparation
{
    public class ShortTagsPreparationPipelineItem: BasePipelineItem
    {
        private readonly List<string> mTargetTags = new List<string>() {
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6"
        };

        protected override void OnRun(ref HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif

            DiscoverNodes(doc.DocumentNode, doc);
        }

        private void DiscoverNodes(HtmlNode rootNode, HtmlDocument doc)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.Name = '{rootNode.Name}'");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerText = {rootNode.InnerText}");
#endif
            if (rootNode.Name == "linktocontent")
            {
                var parentNode = rootNode.ParentNode;

                var linkToContentNodePlace = doc.CreateElement("p");
                parentNode.ReplaceChild(linkToContentNodePlace, rootNode);

                var linkToContentNode = doc.CreateElement("a");
                linkToContentNodePlace.ChildNodes.Add(linkToContentNode);
                linkToContentNode.SetAttributeValue("href", "#Contents");
                linkToContentNode.InnerHtml = "<i class='fas fa-long-arrow-alt-up'></i> back to top";
                return;
            }

            if (rootNode.Name == "see")
            {
                var parentNode = rootNode.ParentNode;

                var href = rootNode.GetAttributeValue("href", string.Empty);

                var linkNode = doc.CreateElement("a");
                parentNode.ReplaceChild(linkNode, rootNode);

                linkNode.SetAttributeValue("href", href);
                linkNode.InnerHtml = "See more for details";

                return;
            }


            if (rootNode.Name == "ico")
            {
                var parentNode = rootNode.ParentNode;

                var targetValue = rootNode.GetAttributeValue("target", string.Empty);

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"targetValue = '{targetValue}'");
#endif
                switch (targetValue)
                {
                    case "Wikipedia":
                        {
                            var wikiNode = doc.CreateElement("i");
                            parentNode.ReplaceChild(wikiNode, rootNode);
                            wikiNode.AddClass("fab fa-wikipedia-w");
                            wikiNode.SetAttributeValue("title", "Wikipedia");
                        }
                        break;

                    default:
                        rootNode.Remove();
                        break;
                }

                return;
            }

            if (mTargetTags.Contains(rootNode.Name))
            {
                var dataHrefValue = rootNode.GetAttributeValue("data-href", string.Empty);

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"dataHrefValue = '{dataHrefValue}'");
#endif
                if (!string.IsNullOrWhiteSpace(dataHrefValue))
                {
                    var href = string.Empty;
                    var name = string.Empty;

                    if (dataHrefValue.StartsWith("#"))
                    {
                        href = dataHrefValue;
                        name = dataHrefValue.Substring(1);
                    }
                    else
                    {
                        href = $"#{dataHrefValue}";
                        name = dataHrefValue;
                    }

#if DEBUG
                    //NLog.LogManager.GetCurrentClassLogger().Info($"href = '{href}'");
                    //NLog.LogManager.GetCurrentClassLogger().Info($"name = '{name}'");
#endif
                    var hrefNode = doc.CreateElement("a");
                    rootNode.ChildNodes.Add(hrefNode);
                    hrefNode.SetAttributeValue("href", href);
                    hrefNode.SetAttributeValue("name", name);
                    hrefNode.SetAttributeValue("title", "The link to the section");
                    hrefNode.AddClass("permalink");
                    hrefNode.InnerHtml = "<i class='fas fa-link'></i>";

                    return;
                }
            }

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                DiscoverNodes(node, doc);
            }
        }
    }
}
