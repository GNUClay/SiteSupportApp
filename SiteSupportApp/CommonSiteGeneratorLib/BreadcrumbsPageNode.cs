using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class BreadcrumbsPageNode
    {
        public BreadcrumbsPageNode Parent { get; set; }
        public bool IsIndex { get; set; }
        public string Path { get; set; }
        public string RelativeHref { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(Parent)} = {Parent?.RelativeHref}");
            sb.AppendLine($"{nameof(IsIndex)} = {IsIndex}");
            sb.AppendLine($"{nameof(Path)} = {Path}");
            sb.AppendLine($"{nameof(RelativeHref)} = {RelativeHref}");
            return sb.ToString();
        }
    }
}
