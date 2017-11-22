using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ClassPage : BaseClassPage
    {
        public ClassPage(NameOfClassNode nameNode, BaseApiPage parent)
            : base(nameNode, parent)
        {
            mNameNode = nameNode;

            Title = $"GNU Clay - {mNameNode.Name} Class";
        }

        private NameOfClassNode mNameNode;

        private List<ClassPage> mClasses = new List<ClassPage>();
        private List<InterfacePage> mInterfaces = new List<InterfacePage>();
        private List<StructPage> mStructs = new List<StructPage>();
        private List<EnumPage> mEnums = new List<EnumPage>();

        protected override void GenerateText()
        {
            foreach (var item in mNameNode.Classes)
            {
                var tmpPage = new ClassPage(item, this);
                tmpPage.Run();
                mClasses.Add(tmpPage);
            }

            foreach (var item in mNameNode.Interfaces)
            {
                var tmpPage = new InterfacePage(item, this);
                tmpPage.Run();
                mInterfaces.Add(tmpPage);
            }

            foreach (var item in mNameNode.Structs)
            {
                var tmpPage = new StructPage(item, this);
                tmpPage.Run();
                mStructs.Add(tmpPage);
            }

            foreach (var item in mNameNode.Enums)
            {
                var tmpPage = new EnumPage(item, this);
                tmpPage.Run();
                mEnums.Add(tmpPage);
            }

            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");
            AppendLine($"<h1>{mNameNode.Name} Class</h1>");
            PrintFirstText();
            PrintClassesList(mClasses);
            PrintInterfacesList(mInterfaces);
            PrintStructsList(mStructs);
            PrintEnumsList(mEnums);
            PrintDelegates(mNameNode.Delegates);
            PrintMembers();
            PrintLastText();

            AppendLine("<p>&nbsp;</p>");

            AppendLine("</article>");
        }
    }
}
