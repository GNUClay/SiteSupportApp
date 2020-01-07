using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class InThePageContentGeneratorPipelineItemFactory : IContentPipelineItemFactory
    {
        public string Name => "InThePageContentGenerator";

        public IContentPipelineItem CreateNewInstance()
        {
            return new InThePageContentGeneratorPipelineItem();
        }
    }
}
