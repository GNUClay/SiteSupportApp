using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class DllPage : BaseApiPage
    {
        public DllPage(string configName, BaseApiPage parent)
            : base(parent)
        {
            var configFullPath = Path.Combine(GeneralSettings.ApiReferenceSourcePath, configName);

            mApiProject = ApiProject.LoadFromFile(configFullPath);

            var targetTypes = mApiProject.targetItems;

            if(mApiProject.useAllItems)
            {
                targetTypes = new List<string>();
            }

            mXMLDocWrapper = new XMLDocWrapper(Path.Combine(GeneralSettings.ApiReferenceSourcePath, mApiProject.fileName), targetTypes);

            mAssemblyName = mXMLDocWrapper.AssemblyName();

            Name = $"{mAssemblyName}.dll";

            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, $"{mAssemblyName.ToLower()}.dll.html");
           
            mSimpleRoot = mXMLDocWrapper.LoadTreeOfTypes().GetNotSimpleNamespace();

            Title = $"GNU Clay - {mAssemblyName}.dll Assembly";
        }

        private ApiProject mApiProject;
        private XMLDocWrapper mXMLDocWrapper;
        private string mAssemblyName;
        private NameOfNamespaceNode mSimpleRoot;
        private List<NamespacePage> mNamespaces = new List<NamespacePage>();

        protected override void GenerateText()
        {
            if(mSimpleRoot.IsRoot)
            {
                foreach (var item in mSimpleRoot.Namespaces)
                {
                    var tmpPage = new NamespacePage(item, this);
                    tmpPage.Run();
                    mNamespaces.Add(tmpPage);
                }
            }
            else
            {
                var tmpPage = new NamespacePage(mSimpleRoot, this);
                tmpPage.Run();
                mNamespaces.Add(tmpPage);
            }

            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");
            AppendLine($"<h1>{mAssemblyName}.dll <span style='font-weight:normal;'>Assembly</span></h1>");

            if(!mSimpleRoot.IsRoot)
            {
                AppendLine($"<p>Root namespace: {mSimpleRoot.FullName}</p>");
            }
            
            if (mNamespaces.Count > 0)
            {
                AppendLine("<h3>Namespaces</h3>");

                AppendLine("<ul>");
                foreach (var item in mNamespaces)
                {
                    AppendLine($"<li><a href='{item.RelativeHref}'>{item.Name}</a></li>");
                }
                AppendLine("</ul>");
            }

            AppendLine("</article>");
        }
    }
}
