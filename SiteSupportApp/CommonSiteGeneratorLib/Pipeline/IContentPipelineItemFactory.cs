using CommonSiteGeneratorLib.SiteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline
{
    public interface IContentPipelineItemFactory
    {
        /// <summary>
        /// Name of this pipeline item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Creates a new instance of this pipeline item and returns the new instance.
        /// </summary>
        /// <returns>A new instance of this pipeline.</returns>
        IContentPipelineItem CreateNewInstance();
    }
}
