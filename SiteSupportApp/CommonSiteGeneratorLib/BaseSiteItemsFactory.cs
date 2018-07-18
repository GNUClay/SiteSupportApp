using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class BaseSiteItemsFactory
    {
        public virtual BaseDirProcesor CreateDirProcessor()
        {
            var result = new BaseDirProcesor(this);
            return result;
        }

        public virtual BasePage CreatePage()
        {
            throw new NotImplementedException();
        }

        public virtual BasePage CreatePageForSpecialProcessing(string specialProcessing)
        {
            throw new NotImplementedException();
        }
    }
}
