using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class InterfacePage : BaseStructElementPage
    {
        public InterfacePage(NameOfClassNode nameNode)
            : base(nameNode)
        {
            mNameNode = nameNode;
        }

        private NameOfClassNode mNameNode;

        protected override void GenerateText()
        {
            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            AppendLine("<article>");
            AppendLine("</article>");
        }
    }
}
