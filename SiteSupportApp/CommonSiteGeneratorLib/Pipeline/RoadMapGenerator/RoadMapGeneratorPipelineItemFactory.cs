using CommonSiteGeneratorLib.SiteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.RoadMapGenerator
{
    public class RoadMapGeneratorPipelineItemFactory : IContentPipelineItemFactory
    {
        public string Name => "RoadMapGenerator";

        public IContentPipelineItem CreateNewInstance()
        {
            return new RoadMapGeneratorPipelineItem();
        }
    }
}
