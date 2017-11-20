using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class EnumPage : BaseStructElementPage
    {
        public EnumPage(NameOfEnumNode nameNode)
            : base(nameNode)
        {
            mNameNode = nameNode;
        }

        private NameOfEnumNode mNameNode;

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
