using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator
{
    public class SiteItemsFactory: BaseSiteItemsFactory
    {
        public override BaseDirProcesor CreateDirProcessor()
        {
            var result = new SiteDirProcesor(this);
            return result;
        }

        public override BasePage CreatePage()
        {
            var result = new TargetPage();
            return result;
        }
    }
}
