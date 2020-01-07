using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.EBNFPreparation
{
    public class EBNFPreparationPipelineItemFactory : IContentPipelineItemFactory
    {
        public string Name => "EBNFPreparation";

        public IContentPipelineItem CreateNewInstance()
        {
            return new EBNFPreparationPipelineItem();
        }
    }
}
