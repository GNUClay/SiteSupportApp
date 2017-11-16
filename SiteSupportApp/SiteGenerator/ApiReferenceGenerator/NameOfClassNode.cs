using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class NameOfClassNode: AbstractNameNode
    {
        public enum KindOfClass
        {
            Class,
            Interface,
            Struct
        }

        public NameOfClassNode(XMLDocWrapper doc, AbstractNameNode parent, string name, KindOfClass kind, List<string> initList)
            : base(doc, parent, name)
        {
            Kind = kind;

            //NLog.LogManager.GetCurrentClassLogger().Info($"constructor Kind = {Kind}");

            if(initList.Count > 0)
            {
                FillChildren(initList);
            }
        }

        public KindOfClass Kind { get; private set; }

        private void FillChildren(List<string> initList)
        {
            var splitedResult = GetSplitedNames(initList);

            var dict = splitedResult.NamesWithTailes;

            foreach (var kvpItem in dict)
            {
                var classNode = new NameOfClassNode(mDoc, this, kvpItem.Key, NameOfClassNode.KindOfClass.Class, kvpItem.Value);
                Classes.Add(classNode);
            }

            ProcessWithoutTailes(splitedResult.WithoutTailes);
        }
    }
}
