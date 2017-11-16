using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class XMLDocWrapper
    {
        public XMLDocWrapper(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                mDocument = XDocument.Load(fs);
                mMembersList = mDocument.Root.Elements().Where(p => p.Name.LocalName.ToLower() == "members").Elements().ToList();
            }
        }

        private XDocument mDocument;
        private List<XElement> mMembersList;

        public List<string> LoadTypeNames()
        {
            var result = new List<string>();

            foreach (var item in mMembersList)
            {
                var name = item.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value?.Trim();

                if(string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                if(!name.StartsWith("T:"))
                {
                    continue;
                }

                result.Add(name.Replace("T:", "").Trim());
            }

            result = result.Distinct().ToList();

            return result;
        }

        public NameOfNamespaceNode LoadTreeOfTypes()
        {
            var types = LoadTypeNames();

            var node = new NameOfNamespaceNode(types, null, this, null);
            return node;
        }

        public bool HasSummary(string key)
        {
            var targetMember = GetMember(key);

            if (targetMember == null)
            {
                return false;
            }

            return targetMember.Elements().Any(p => p.Name.LocalName.ToLower() == "summary");
        }

        public string GetSummaryType(string key)
        {
            var targetMember = GetMember(key);

            if(targetMember == null)
            {
                return string.Empty;
            }

            var summary = targetMember.Elements().Where(p => p.Name.LocalName.ToLower() == "summary").FirstOrDefault(p => p.Attributes().Any(x => x.Name.LocalName == "type"));

            if(summary == null)
            {
                return string.Empty;
            }

            return summary.Attributes().FirstOrDefault(p => p.Name.LocalName == "type")?.Value?.Trim();
        }

        public XElement GetMember(string key)
        {
            if(!key.StartsWith("T:"))
            {
                key = $"T:{key}";
            }

            return mMembersList.FirstOrDefault(p => p.Attributes().FirstOrDefault(x => x.Name.LocalName == "name").Value.Trim() == key);
        }
    }
}
