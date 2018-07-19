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

        private Dictionary<string, BreadcrumbsPageNode> mBreadcrumbsPageNodesDict = new Dictionary<string, BreadcrumbsPageNode>();

        public void SetBreadcrumbsPageNodes(List<BreadcrumbsPageNode> pagesList)
        {
            mBreadcrumbsPageNodesDict = pagesList.GroupBy(p => p.Path).ToDictionary(p =>p.Key, p => p.First());
        }

        public BreadcrumbsPageNode GetBreadcrumbsPageNode(string path)
        {
            if(mBreadcrumbsPageNodesDict.ContainsKey(path))
            {
                return mBreadcrumbsPageNodesDict[path];
            }

            return null;
        }
    }
}
