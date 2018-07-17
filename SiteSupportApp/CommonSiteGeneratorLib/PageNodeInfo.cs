using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class PageNodeInfo
    {
        public string SourceName { get; set; } = string.Empty;
        public string TargetDirName { get; set; } = string.Empty;
        public string FileNameWithOutExtension { get; set; } = string.Empty;
    }
}
