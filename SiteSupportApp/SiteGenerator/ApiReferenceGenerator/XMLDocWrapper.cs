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
                NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation item = {item.ToString()}");
                NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation item = {item.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value}");
            }

            return result;
        }
    }
}
