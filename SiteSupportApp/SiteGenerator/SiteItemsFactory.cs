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

        public override BasePage CreatePageForSpecialProcessing(string specialProcessing)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"CreatePageForSpecialProcessing specialProcessing = {specialProcessing}");
#endif

            if (specialProcessing == "index")
            {
                var result = new IndexPage(this);
                return result;
            }

            throw new NotImplementedException();
        }

        public override BasePage CreatePage()
        {
            var result = new TargetPage(this);
            return result;
        }
    }
}
