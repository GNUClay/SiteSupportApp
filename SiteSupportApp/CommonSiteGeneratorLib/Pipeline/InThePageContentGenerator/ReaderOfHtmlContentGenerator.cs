using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class ReaderOfHtmlContentGenerator
    {
        private readonly string mContentPlaceTag = "ContentsPlace".ToLower();
        private readonly List<string> mTargetTags = new List<string>() {
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6"
        };

        public ReaderResultOfHtmlContentGenerator Read(HtmlDocument document)
        {
            var result = new ReaderResultOfHtmlContentGenerator();

            var context = new ContextReaderOfHtmlContentGenerator();

            DiscoverNodes(document.DocumentNode, context);

            result.ContentPlaceNode = context.ContentPlaceNode;

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"context.ContentItemsList = {JsonConvert.SerializeObject(context.ContentItemsList, Formatting.Indented)}");
#endif
            var item = MakeTree(context);

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"item = {JsonConvert.SerializeObject(item, Formatting.Indented)}");
#endif
            result.Items = item.Items;

            return result;
        }

        private void DiscoverNodes(HtmlNode rootNode, ContextReaderOfHtmlContentGenerator context)
        {
            if (context.NeedProcess)
            {
                if (mTargetTags.Contains(rootNode.Name))
                {
                    ProcessGettingLink(rootNode, context);
                }
                else
                {
                    foreach (var node in rootNode.ChildNodes.ToList())
                    {
                        DiscoverNodes(node, context);
                    }
                }
            }
            else
            {
                if (rootNode.Name.ToLower() == mContentPlaceTag)
                {
                    context.NeedProcess = true;
                    context.ContentPlaceNode = rootNode;
                }
                else
                {
                    foreach (var node in rootNode.ChildNodes.ToList())
                    {
                        DiscoverNodes(node, context);
                    }
                }
            }
        }

        private void ProcessGettingLink(HtmlNode rootNode, ContextReaderOfHtmlContentGenerator context)
        {
            var contentItem = new ContentItem()
            {
                TagName = rootNode.Name
            };

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.Name = '{rootNode.Name}'");
            NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
            NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
            NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerText = {rootNode.InnerText}");
#endif
            var text = rootNode.InnerText.Trim();

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"text = '{text}'");
#endif
            contentItem.Title = text;

            var hrefNode = rootNode.ChildNodes.SingleOrDefault(p => p.Name == "a" && !string.IsNullOrWhiteSpace(p.GetAttributeValue("name", string.Empty)));

            if (hrefNode != null)
            {
                var href = hrefNode.GetAttributeValue("href", string.Empty);

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"href = '{href}'");
#endif
                contentItem.Href = href;
            }

            context.ContentItemsList.Add(contentItem);
        }

        private ContentItem MakeTree(ContextReaderOfHtmlContentGenerator context)
        {
            var item = new ContentItem();

            if (!context.ContentItemsList.Any())
            {
                return item;
            }

            var queue = new Queue<ContentItem>(context.ContentItemsList);

            if (queue.Peek().TagName != "h1")
            {
                throw new Exception("Contents should start from H1!");
            }

            ProcessH1Items(item, queue);

            return item;
        }

        private void ProcessH1Items(ContentItem parentItem, Queue<ContentItem> queue)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info("Begin");
#endif
            ContentItem currItem = null;

            while (queue.Count > 0)
            {
                var currentSourceItem = queue.Peek();

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"currentSourceItem = {JsonConvert.SerializeObject(currentSourceItem, Formatting.Indented)}");
#endif
                switch (currentSourceItem.TagName)
                {
                    case "h1":
                        queue.Dequeue();
                        currItem = new ContentItem()
                        {
                            TagName = currentSourceItem.TagName,
                            Title = currentSourceItem.Title,
                            Href = currentSourceItem.Href
                        };
                        parentItem.Items.Add(currItem);
                        break;

                    case "h2":
                        ProcessH2Items(currItem, queue);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info("End");
#endif
        }

        private void ProcessH2Items(ContentItem parentItem, Queue<ContentItem> queue)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info("Begin");
#endif
            ContentItem currItem = null;

            while (queue.Count > 0)
            {
                var currentSourceItem = queue.Peek();

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"currentSourceItem = {JsonConvert.SerializeObject(currentSourceItem, Formatting.Indented)}");
#endif
                switch (currentSourceItem.TagName)
                {
                    case "h1":
                        return;

                    case "h2":
                        queue.Dequeue();
                        currItem = new ContentItem()
                        {
                            TagName = currentSourceItem.TagName,
                            Title = currentSourceItem.Title,
                            Href = currentSourceItem.Href
                        };
                        parentItem.Items.Add(currItem);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info("End");
#endif
        }
    }
}
