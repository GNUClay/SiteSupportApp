using CommonSiteGeneratorLib.SiteData;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class InThePageContentGeneratorPipelineItem : BasePipelineItem
    {
        private readonly ReaderOfHtmlContentGenerator mReader = new ReaderOfHtmlContentGenerator();

        protected override void OnRun(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif

            var contentsInfo = mReader.Read(doc);

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"contentsInfo.Items = {JsonConvert.SerializeObject(contentsInfo.Items, Formatting.Indented)}");

            //NLog.LogManager.GetCurrentClassLogger().Info($"contentsInfo.ContentPlaceNode = {contentsInfo.ContentPlaceNode?.OuterHtml}");
#endif
            CreateContents(contentsInfo, doc);
        }

        private void CreateContents(ReaderResultOfHtmlContentGenerator contentsInfo, HtmlDocument doc)
        {
            var contentNodePlace = contentsInfo.ContentPlaceNode;

            if(contentNodePlace == null)
            {
                return;
            }

            var parentOfContent = contentNodePlace.ParentNode;

            var rootContentNode = doc.CreateElement("p");
            parentOfContent.ReplaceChild(rootContentNode, contentNodePlace);

            var contentsHeaderNode = doc.CreateElement("h1");
            parentOfContent.InsertBefore(contentsHeaderNode, rootContentNode);
            contentsHeaderNode.AddClass("center-h");

            contentsHeaderNode.InnerHtml = @"    Content
    <a href='#Contents' name='Contents' class='permalink' title='The link to the section'>
        <i class='fas fa-link'></i>
    </a>";

            if (contentsInfo.Items.Any())
            {
                CreateChildItems(rootContentNode, contentsInfo.Items, doc);
            }
        }

        private void CreateContentsLevel(HtmlNode parentNode, ContentItem item, HtmlDocument doc)
        {
            var itemNode = doc.CreateElement("a");
            parentNode.ChildNodes.Add(itemNode);
            itemNode.SetAttributeValue("href", item.Href);
            itemNode.InnerHtml = item.Title;
            if (item.Items.Any())
            {
                CreateChildItems(parentNode, item.Items, doc);
            }
        }

        private void CreateChildItems(HtmlNode parentNode, List<ContentItem> items, HtmlDocument doc)
        {
            var rootNode = doc.CreateElement("ul");
            parentNode.ChildNodes.Add(rootNode);

            foreach (var item in items)
            {
                var liNode = doc.CreateElement("li");
                rootNode.ChildNodes.Add(liNode);
                CreateContentsLevel(liNode, item, doc);
            }
        }
    }
}
