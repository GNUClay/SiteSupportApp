using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSiteGeneratorLib.SiteData;
using HtmlAgilityPack;

namespace CommonSiteGeneratorLib.Pipeline.EBNFPreparation
{
    public class EBNFPreparationPipelineItem : BasePipelineItem
    {
        protected override HtmlDocument OnPrepareDoc(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
            var newDoc2 = EBNFTemplatesResolver.Run(doc);

            var resultFileName = Path.Combine(GeneralSettings.TempPath, $"{Guid.NewGuid().ToString("D")}.html");

            newDoc2.Save(resultFileName);

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"resultFileName = '{resultFileName}'");
#endif

            var newDoc = new HtmlDocument();
            newDoc.Load(resultFileName);

            return newDoc;
        }

        protected override void OnRun(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"EBNFPreparationPipelineItem OnRun sitePageInfo = {sitePageInfo}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"EBNFPreparationPipelineItem OnRun pagePluginInfo = {pagePluginInfo}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"EBNFPreparationPipelineItem OnRun rootNode.OuterHtml = {doc.DocumentNode.OuterHtml}");
#endif

            DiscoverNodes(doc.DocumentNode, doc);

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"EBNFPreparationPipelineItem OnRun rootNode.OuterHtml (after) = {doc.DocumentNode.OuterHtml}");
#endif
        }

        private void DiscoverNodes(HtmlNode rootNode, HtmlDocument doc)
        {
#if DEBUG
            //if(rootNode.Name != "#document")
            //{
            //    NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.Name = '{rootNode.Name}'");
            //    NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
            //    NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
            //    NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerText = {rootNode.InnerText}");
            //}
#endif

            if (rootNode.Name == "ebnfcdecl")
            {
                var parentNode = rootNode.ParentNode;

                var name = rootNode.GetAttributeValue("name", string.Empty);
#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"name = '{name}'");
#endif
                var linkNode = doc.CreateElement("a");
                parentNode.ReplaceChild(linkNode, rootNode);

                linkNode.SetAttributeValue("href", $"#{name}");
                linkNode.SetAttributeValue("name", name);
                linkNode.InnerHtml = name;

                return;
            }

            if (rootNode.Name == "ebnfc")
            {
                var parentNode = rootNode.ParentNode;

                var name = rootNode.GetAttributeValue("name", string.Empty);
#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"name = '{name}'");
#endif
                var linkNode = doc.CreateElement("a");
                parentNode.ReplaceChild(linkNode, rootNode);

                linkNode.SetAttributeValue("href", $"#{name}");
                linkNode.InnerHtml = name;

                return;
            }

            if (rootNode.Name == "a")
            {
                var name = rootNode.GetAttributeValue("name", string.Empty);
                var href = rootNode.GetAttributeValue("href", string.Empty);

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"name = '{name}'");
                //NLog.LogManager.GetCurrentClassLogger().Info($"href = '{href}'");
#endif
                if (string.IsNullOrWhiteSpace(href) && !string.IsNullOrWhiteSpace(name))
                {
                    if (name.StartsWith("#"))
                    {
                        href = name;
                    }
                    else
                    {
                        href = $"#{name}";
                    }

                    rootNode.SetAttributeValue("href", href);
                }

                return;
            }

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                DiscoverNodes(node, doc);
            }
        }
    }
}
