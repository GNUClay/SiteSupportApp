using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPackagePublisher
{
    public class PackageInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string NuspecName { get; set; }
        public string ProjectName { get; set; }
        public string Version { get; set; }
        public string ShortVersion { get; set; }
        public string PackageName { get; set; }
        public string PackageFullName { get; set; }
        public string AlternatePackageName { get; set; }
        public string AlternatePackageFullName { get; set; }
    }
}
