using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class ApiProject
    {
        public bool useAllItems { get; set; }
        public List<string> targetItems { get; set; }
    }
}
