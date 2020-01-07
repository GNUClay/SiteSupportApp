using CommonSiteGeneratorLib.Pipeline;
using CommonSiteGeneratorLib.Pipeline.EBNFPreparation;
using CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator;
using CommonSiteGeneratorLib.Pipeline.ShortTagsPreparation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class BaseSiteItemsFactory
    {
        public BaseSiteItemsFactory()
        {
            RegisterPreinstalledPlugins();
        }

        protected void RegisterPreinstalledPlugins()
        {
            RegisterPlugin(new EBNFPreparationPipelineItemFactory());
            RegisterPlugin(new ShortTagsPreparationPipelineItemFactory());
            RegisterPlugin(new InThePageContentGeneratorPipelineItemFactory());
        }

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

        public void RegisterPlugin(IContentPipelineItemFactory factory)
        {
            if(factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if(string.IsNullOrWhiteSpace(factory.Name))
            {
                throw new NullReferenceException("Name of page pipeline item can not be null or empty.");
            }

            mPluginsDict[factory.Name] = factory;
        }

        public IContentPipelineItem GetPipelineItem(string name)
        {
            if(!mPluginsDict.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Plugin `{name}` is not registered yet.");
            }
            
            return mPluginsDict[name].CreateNewInstance();
        }

        private readonly Dictionary<string, IContentPipelineItemFactory> mPluginsDict = new Dictionary<string, IContentPipelineItemFactory>();
    }
}
