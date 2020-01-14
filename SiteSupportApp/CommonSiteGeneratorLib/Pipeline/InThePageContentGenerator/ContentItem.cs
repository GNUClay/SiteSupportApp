using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class ContentItem
    {
        public string TagName { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public List<ContentItem> Items { get; set; } = new List<ContentItem>();
    }
}
