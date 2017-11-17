using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ClassInfo
    {
        public ClassInfo(XMLDocWrapper doc, string fullName, KindOfClass kind)
        {
            mDoc = doc;
            FullName = fullName;
            Kind = kind;

            FillMembers();
        }

        private XMLDocWrapper mDoc;
        public string FullName { get; private set; }
        public KindOfClass Kind { get; private set; }

        public List<string> Properties { get; private set; } = new List<string>();
        public List<string> Methods { get; private set; } = new List<string>();
        public List<string> Events { get; private set; } = new List<string>();

        public string KindName
        {
            get
            {
                switch (Kind)
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

        private void FillMembers()
        {
            Properties = mDoc.GetPropertiesNames(FullName);
            Methods = mDoc.GetMethodsNames(FullName);
            Events = mDoc.GetEventsNames(FullName);
        }

        public string DisplayMembers()
        {
            var ident = 4;
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"Begin {KindName}:{FullName}");
            sb.AppendLine($"{spaces}Properties:");
            foreach(var member in Properties)
            {
                sb.AppendLine($"{nextSpaces}{member};");
            }     
            sb.AppendLine($"{spaces}Methods:");
            foreach (var member in Methods)
            {
                sb.AppendLine($"{nextSpaces}{member};");
            }
            sb.AppendLine($"{spaces}Events:");
            foreach (var member in Events)
            {
                sb.AppendLine($"{nextSpaces}{member};");
            }
            sb.AppendLine($"End {KindName}:{FullName}");
            return sb.ToString();
        }
    }
}
