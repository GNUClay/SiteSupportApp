﻿using CommonSiteGeneratorLib.SiteData;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class InThePageContentGeneratorPipelineItem : BasePipelineItem
    {
        protected override void OnRun(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif
        }
    }
}
