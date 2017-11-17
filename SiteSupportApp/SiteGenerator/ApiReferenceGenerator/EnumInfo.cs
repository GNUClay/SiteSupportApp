using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class EnumInfo
    {
        public EnumInfo(XMLDocWrapper doc, string fullName)
        {
            mDoc = doc;
            FullName = fullName;

            FillMembers();
        }

        private XMLDocWrapper mDoc;
        public string FullName { get; private set; }
        public List<string> Items { get; private set; } = new List<string>();

        private void FillMembers()
        {
            Items = mDoc.GetEnumElementsNames(FullName);
        }

        public string DisplayItems()
        {
            var ident = 4;
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"Begin enum:{FullName}");
            sb.AppendLine($"{spaces}Items:");
            foreach (var member in Items)
            {
                sb.AppendLine($"{nextSpaces}{member};");
            }
            sb.AppendLine($"End enum:{FullName}");
            return sb.ToString();
        }
    }
}
