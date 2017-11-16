using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ClassInfo
    {
        public ClassInfo(XMLDocWrapper doc, string fullName)
        {
            mDoc = doc;
            FullName = fullName;

            FillMembers();
        }

        private XMLDocWrapper mDoc;
        public string FullName { get; private set; }

        public List<string> Properties { get; private set; } = new List<string>();
        public List<string> Methods { get; private set; } = new List<string>();
        public List<string> Events { get; private set; } = new List<string>();

        private void FillMembers()
        {
            var propertiesList = mDoc.GetPropertiesNames(FullName);

            foreach(var item in propertiesList)
            {
                Properties.Add(item);
            }

            var methodsList = mDoc.GetMethodsNames(FullName);

            foreach (var item in methodsList)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"FillMembers Method item = {item}");
            }

            var eventsList = mDoc.GetEventsNames(FullName);

            foreach (var item in eventsList)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"FillMembers Events item = {item}");
            }
        }
    }
}
