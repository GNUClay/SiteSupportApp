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
            }
        }

        private XDocument mDocument;

        public List<string> LoadTypeNames()
        {
            var result = new List<string>();

            var members = mDocument.Root.Elements().Where(p => p.Name.LocalName.ToLower() == "members").Elements().ToList();

            foreach (var item in members)
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
    }
}
