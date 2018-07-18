using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatypemanSiteSupport
{
    public class SiteCreator: BaseCreator
    {
        protected override BaseSiteItemsFactory GetsiteItemsFactory()
        {
            var result = new SiteItemsFactory();
            return result;
        }
    }
}
