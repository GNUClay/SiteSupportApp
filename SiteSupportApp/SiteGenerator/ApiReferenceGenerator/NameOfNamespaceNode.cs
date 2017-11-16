using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class NameOfNamespaceNode: AbstractNameNode
    {
        public NameOfNamespaceNode(List<string> initList, string name, XMLDocWrapper doc, NameOfNamespaceNode parent)
            : base(doc, parent, name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                IsRoot = true;
            }

            FillChildren(initList);
        }

        public bool IsRoot { get; private set; }
        public List<NameOfNamespaceNode> Namespaces { get; set; } = new List<NameOfNamespaceNode>();

        private void FillChildren(List<string> initList)
        {
            var splitedResult = GetSplitedNames(initList);

            var dict = splitedResult.NamesWithTailes;

            foreach(var kvpItem in dict)
            {
                var fullTypeName = $"{FullName}.{kvpItem.Key}";

                //NLog.LogManager.GetCurrentClassLogger().Info($"FillChildren Process fullTypeName = {fullTypeName}");

                if (mDoc.HasSummary(fullTypeName))
                {
                    var classNode = new NameOfClassNode(mDoc, this, kvpItem.Key, NameOfClassNode.KindOfClass.Class, kvpItem.Value);
                    Classes.Add(classNode);
                }
                else
                {
                    var namespaceNode = new NameOfNamespaceNode(kvpItem.Value, kvpItem.Key, mDoc, this);
                    Namespaces.Add(namespaceNode);
                }
            }

            ProcessWithoutTailes(splitedResult.WithoutTailes);
        }
    }
}
