using CommonUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public static class ConfigAppSettingsHelper
    {
        private static readonly string[] mAllKeys;
        private static readonly Dictionary<string, string> mValueCache = new Dictionary<string, string>();

        static ConfigAppSettingsHelper()
        {
            mAllKeys = ConfigurationManager.AppSettings.AllKeys;
        }

        public static string GetExistingFileName(string key)
        {
            return GetValue(key, (string targetKey, string val) => File.Exists(val), true);
        }

        public static string GetExistingDirectoryName(string key)
        {
            return GetValue(key, (string targetKey, string val) => Directory.Exists(val), true);
        }

        public static string GetValue(string key, Func<string, string, bool> fitFun, bool useEvNormalize)
        {
            if (mValueCache.ContainsKey(key))
            {
                return mValueCache[key];
            }

            var primaryValue = ConfigurationManager.AppSettings[key];

            if(useEvNormalize)
            {
                primaryValue = EVPath.Normalize(primaryValue);
            }

            if (fitFun(key, primaryValue))
            {
                mValueCache[key] = primaryValue;
                return primaryValue;
            }

            var targetKeys = mAllKeys.Where(p => p.StartsWith(key)).ToList();
            var resultKeys = new List<KeyValuePair<int, string>>();

            foreach (var targetKey in targetKeys)
            {
                var tail = targetKey.Replace(key, string.Empty);

                if (tail.StartsWith("_"))
                {
                    tail = tail.Substring(1);
                }

                if (string.IsNullOrWhiteSpace(tail))
                {
                    continue;
                }

                if (int.TryParse(tail, out int num))
                {
                    resultKeys.Add(new KeyValuePair<int, string>(num, targetKey));
                }
            }

            if (!resultKeys.Any())
            {
                mValueCache[key] = string.Empty;
                return string.Empty;
            }

            resultKeys = resultKeys.OrderBy(p => p.Key).ToList();

            foreach (var resultKey in resultKeys)
            {
                var secondaryValue = ConfigurationManager.AppSettings[resultKey.Value];

                if(useEvNormalize)
                {
                    secondaryValue = EVPath.Normalize(secondaryValue);
                }

                if (fitFun(resultKey.Value, secondaryValue))
                {
                    mValueCache[key] = secondaryValue;
                    return secondaryValue;
                }
            }

            mValueCache[key] = string.Empty;
            return string.Empty;
        }
    }
}
