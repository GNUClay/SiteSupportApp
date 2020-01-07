using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.ShortTagsPreparation
{
    public class ShortTagsPreparationPipelineItemFactory : IContentPipelineItemFactory
    {
        public string Name => "ShortTagsPreparation";

        public IContentPipelineItem CreateNewInstance()
        {
            return new ShortTagsPreparationPipelineItem();
        }
    }
}
