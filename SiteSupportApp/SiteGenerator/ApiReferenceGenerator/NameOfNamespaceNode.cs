using CommonUtils;
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

        public override string KindName => "namespace";

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
                    var classNode = new NameOfClassNode(mDoc, this, kvpItem.Key, KindOfClass.Class, kvpItem.Value);
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

        public string DisplayHierarchy()
        {
            return DisplayHierarchy(0);
        }

        public override string DisplayHierarchy(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin {KindName}:{Name}");
            foreach (var item in Namespaces)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            foreach(var item in Classes)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            foreach (var item in Interfaces)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            foreach (var item in Structs)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            foreach (var item in Delegates)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            foreach (var item in Enums)
            {
                sb.Append(item.DisplayHierarchy(nextIdent));
            }
            sb.AppendLine($"{spaces}End {KindName}:{Name}");
            return sb.ToString();
        }
    }
}
