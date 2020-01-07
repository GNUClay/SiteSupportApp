using CommonSiteGeneratorLib.SiteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline
{
    public interface IContentPipelineItem
    {
        /// <summary>
        /// Processes site's source file.
        /// </summary>
        /// <param name="sourceFileName">Name of site's source file.</param>
        /// <param name="sitePageInfo">Settings of the page.</param>
        /// <param name="pagePluginInfo">Information for worting this plugin.</param>
        /// <returns>Name of file with results of this processing.</returns>
        string Run(string sourceFileName, SitePageInfo sitePageInfo, PagePluginInfo pagePluginInfo);
    }
}
