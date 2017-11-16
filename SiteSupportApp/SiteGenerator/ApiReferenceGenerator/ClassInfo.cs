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

        private void FillMembers()
        {
            var propertiesList = mDoc.GetPropertiesNames(FullName);

            foreach(var item in propertiesList)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"GetPropertiesNames item = {item }");
            }
        }
    }
}
