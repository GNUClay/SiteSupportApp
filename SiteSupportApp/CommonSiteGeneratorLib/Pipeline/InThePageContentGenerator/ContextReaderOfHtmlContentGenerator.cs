using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.InThePageContentGenerator
{
    public class ContextReaderOfHtmlContentGenerator
    {
        public bool NeedProcess;
        public List<ContentItem> ContentItemsList = new List<ContentItem>();
        public HtmlNode ContentPlaceNode { get; set; }
    }
}
