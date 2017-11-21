using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ClassPage : BaseStructElementPage
    {
        public ClassPage(NameOfClassNode nameNode, BaseApiPage parent)
            : base(nameNode, parent)
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
            GenerateNavBar();

            AppendLine("<article>");
            //PrintClassesList(mClasses);
            //PrintInterfacesList(mInterfaces);
            //PrintStructsList(mStructs);
            //PrintEnumsList(mEnums);
            //PrintDelegates(mNameNode.Delegates);

            AppendLine("</article>");
        }
    }
}
