using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiteGenerator.DirProcesor;

namespace SiteGenerator.ApiReferenceGenerator
{
    public static class ApiDirProcessor
    {
        public static void Run(SiteNodeInfo info)
        {
            if (!Directory.Exists(info.TargetDirName))
            {
                Directory.CreateDirectory(info.TargetDirName);
            }

            var tmpIndexPage = new IndexPage();
            tmpIndexPage.Run();
        }
    }
}
