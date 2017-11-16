using CommonUtils;
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

        public override string KindName
        {
            get
            {
                switch(Kind)
                {
                    case KindOfClass.Class:
                        return "class";

                    case KindOfClass.Interface:
                        return "interface";

                    case KindOfClass.Struct:
                        return "struct";

                    default:
                        throw new ArgumentOutOfRangeException(nameof(Kind), Kind, null);
                }
            }
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

        public override string DisplayHierarchy(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin {KindName}:{Name}");
            foreach (var item in Classes)
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
