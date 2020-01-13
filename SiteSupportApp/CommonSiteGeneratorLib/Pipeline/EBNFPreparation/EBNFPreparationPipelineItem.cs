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
        protected override void OnRun(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif

            DiscoverNodes(doc.DocumentNode);
        }

        private void DiscoverNodes(HtmlNode rootNode)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.Name = '{rootNode.Name}'");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"rootNode.InnerText = {rootNode.InnerText}");
#endif

            if (rootNode.Name == "a")
            {
                var name = rootNode.GetAttributeValue("name", string.Empty);
                var href = rootNode.GetAttributeValue("href", string.Empty);

#if DEBUG
                NLog.LogManager.GetCurrentClassLogger().Info($"name = '{name}'");
                NLog.LogManager.GetCurrentClassLogger().Info($"href = '{href}'");
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
                DiscoverNodes(node);
            }
        }
    }
}
