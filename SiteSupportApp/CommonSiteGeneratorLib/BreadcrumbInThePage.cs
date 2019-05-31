using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class BreadcrumbInThePage
    {
        public string Title { get; set; }
        public string Href { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(Title)} = {Title}");
            sb.AppendLine($"{nameof(Href)} = {Href}");
            return sb.ToString();
        }
    }
}
