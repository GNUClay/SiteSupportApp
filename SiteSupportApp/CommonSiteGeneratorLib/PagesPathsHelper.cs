using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public static class PagesPathsHelper
    {
        public static string PathToRelativeHref(string path)
        {
            var pos = path.IndexOf(GeneralSettings.SiteName);
            return path.Substring(pos).Replace(GeneralSettings.SiteName, string.Empty).ToLower();
        }

        public static string RelativeHrefToAbsolute(string relativeHref)
        {


            return string.Empty;
        }
    }
}
