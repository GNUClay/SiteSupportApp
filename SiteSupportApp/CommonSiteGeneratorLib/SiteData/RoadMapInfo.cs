using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.SiteData
{
    public class RoadMapItemInfo
    {
        public List<RoadMapItemInfo> Items { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class RoadMapInfo
    {
        public List<RoadMapItemInfo> Items { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static RoadMapInfo LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<RoadMapInfo>(File.ReadAllText(path));
        }
    }
}
