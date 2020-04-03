using CommonSiteGeneratorLib.SiteData;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline
{
    public abstract class BasePipelineItem: IContentPipelineItem
    {
        public virtual string Run(string sourceFileName, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run sourceFileName = {sourceFileName}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run sitePageInfo = {sitePageInfo}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run pagePluginInfo = {pagePluginInfo}");
#endif

            var doc = new HtmlDocument();
            doc.Load(sourceFileName);

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run doc.Text = {doc.Text}");
#endif

            OnRun(ref doc, sitePageInfo, pagePluginInfo);

            var fileInfo = new FileInfo(sourceFileName);
            
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run fileInfo.Extension = {fileInfo.Extension}");
#endif

            var resultFileName = Path.Combine(GeneralSettings.TempPath, $"{Guid.NewGuid().ToString("D")}{fileInfo.Extension}");

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"Run resultFileName = {resultFileName}");
#endif

            doc.Save(resultFileName);

            return resultFileName;
        }

        //protected virtual HtmlDocument OnPrepareDoc(HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo)
        //{
        //    return null;
        //}

        protected abstract void OnRun(ref HtmlDocument doc, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo);
    }
}
