using CommonSiteGeneratorLib;
using SiteGenerator.ApiReferenceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator
{
    public class SiteDirProcesor: BaseDirProcesor
    {
        public SiteDirProcesor(BaseSiteItemsFactory siteItemsFactory)
            : base(siteItemsFactory)
        {
        }

        public override void Run(SiteNodeInfo info)
        {
            if (info.SourceDirName == GeneralSettings.ApiReferenceSourcePath)
            {
                //ApiDirProcessor.Run(info);
                return;
            }

            base.Run(info);
        }
    }
}
