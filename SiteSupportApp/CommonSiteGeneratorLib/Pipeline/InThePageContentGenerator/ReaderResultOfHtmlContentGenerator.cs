using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class ReaderResultOfHtmlContentGenerator
    {
        public HtmlNode ContentPlaceNode { get; set; }
        public List<ContentItem> Items { get; set; } = new List<ContentItem>();
    }
}
