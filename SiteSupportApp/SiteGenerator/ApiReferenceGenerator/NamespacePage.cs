using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class NamespacePage: BaseStructElementPage
    {
        public NamespacePage(NameOfNamespaceNode nameNode)
            : base(nameNode)
        {
            mNameNode = nameNode;
        }

        private NameOfNamespaceNode mNameNode;

        protected override void GenerateText()
        {
            foreach (var item in mNameNode.Namespaces)
            {
                var tmpPage = new NamespacePage(item);
                tmpPage.Run();
            }

            foreach (var item in mNameNode.Classes)
            {
                var tmpPage = new ClassPage(item);
                tmpPage.Run();
            }

            foreach (var item in mNameNode.Interfaces)
            {
                var tmpPage = new InterfacePage(item);
                tmpPage.Run();
            }

            foreach (var item in mNameNode.Structs)
            {
                var tmpPage = new StructPage(item);
                tmpPage.Run();
            }

            foreach (var item in mNameNode.Enums)
            {
                var tmpPage = new EnumPage(item);
                tmpPage.Run();
            }

            foreach (var item in mNameNode.Delegates)
            {
                var tmpPage = new DelegatePage(item);
                tmpPage.Run();
            }

            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            AppendLine("<article>");
            AppendLine("</article>");
        }
    }
}
