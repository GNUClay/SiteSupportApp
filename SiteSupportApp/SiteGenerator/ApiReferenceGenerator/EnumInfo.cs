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
    }
}
