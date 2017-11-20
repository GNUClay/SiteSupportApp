using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ApiProject
    {
        public string fileName { get; set; }
        public bool useAllItems { get; set; }
        public List<string> targetItems { get; set; }

        public static ApiProject LoadFromFile(string path)
        {
            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return new ApiProject();
                }

                using (var reader = new StreamReader(tmpfile))
                {
                    var content = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<ApiProject>(content);
                }
            }
        }
    }
}
