using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class InterfacePage : BaseClassPage
    {
        public InterfacePage(NameOfClassNode nameNode, BaseApiPage parent, BaseSiteItemsFactory factory)
            : base(nameNode, parent, factory)
        {
            mNameNode = nameNode;

            Title = $"GNU Clay - {mNameNode.Name} Interface";
        }

        private NameOfClassNode mNameNode;

        protected override void GenerateText()
        {
            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");
            AppendLine($"<h1>{mNameNode.Name} Interface</h1>");
            PrintFirstText();
            PrintMembers();
            PrintLastText();

            AppendLine("<p>&nbsp;</p>");
            AppendLine("</article>");
        }
    }
}
