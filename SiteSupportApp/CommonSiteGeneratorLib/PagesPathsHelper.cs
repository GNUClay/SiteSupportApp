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
            var domainHref = $"https://{GeneralSettings.SiteName}";

#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"RelativeHrefToAbsolute relativeHref = {relativeHref}");
            //NLog.LogManager.GetCurrentClassLogger().Info($"RelativeHrefToAbsolute domainHref = {domainHref}");
#endif

            if(relativeHref.StartsWith(domainHref))
            {
                return domainHref.Replace("\\", "/");
            }

            return $"{domainHref}{relativeHref}".Replace("\\", "/");
        }
    }
}
