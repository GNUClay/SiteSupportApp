using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSiteGeneratorLib.SiteData;

namespace CommonSiteGeneratorLib.Pipeline.EBNFPreparation
{
    public class EBNFPreparationPipelineItem : IContentPipelineItem
    {
        public string Run(string sourceFileName, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sourceFileName = {sourceFileName}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif

            return sourceFileName;//tmp
        }
    }
}
