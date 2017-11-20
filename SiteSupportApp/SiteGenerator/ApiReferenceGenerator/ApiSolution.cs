using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ApiSolution
    {
        public List<string> items;

        public static ApiSolution LoadFromFile(string path)
        {
            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return new ApiSolution();
                }

                using (var reader = new StreamReader(tmpfile))
                {
                    return JsonConvert.DeserializeObject<ApiSolution>(reader.ReadToEnd());
                }
            }
        }
    }
}
