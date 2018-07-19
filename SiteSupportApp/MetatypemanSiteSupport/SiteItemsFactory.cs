using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatypemanSiteSupport
{
    public class SiteItemsFactory: BaseSiteItemsFactory
    {
        public override BasePage CreatePageForSpecialProcessing(string specialProcessing)
        {
#if DEBUG
            NLog.LogManager.GetCurrentClassLogger().Info($"CreatePageForSpecialProcessing specialProcessing = {specialProcessing}");
#endif

            if(specialProcessing == "index")
            {
                var result = new IndexPage();
                return result;
            }

            if(specialProcessing == "index_of_articles")
            {
                var result = new ArticleIndexPage();
                return result;
            }

            throw new NotImplementedException();
        }

        public override BasePage CreatePage()
        {
            var result = new ContentPage();
            return result;
        }
    }
}
