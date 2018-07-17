using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator
{
    public class SiteCreator: BaseCreator
    {
        public SiteCreator()
        {
        }

        protected override BaseSiteItemsFactory GetsiteItemsFactory()
        {
            var result = new SiteItemsFactory();
            return result;
        }
    }
}
