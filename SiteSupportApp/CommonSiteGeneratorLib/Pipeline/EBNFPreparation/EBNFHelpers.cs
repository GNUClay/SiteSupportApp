using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.EBNFPreparation
{
    public static class EBNFHelpers
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static List<string> ParseGrammarBlock(string html)
        {
            var itemsStrList = html.Split(new string[1] { $".{Environment.NewLine}"  }, StringSplitOptions.RemoveEmptyEntries).Where(p => !string.IsNullOrWhiteSpace(p));

            var result = new List<string>();
            var buffer = new List<string>();

            foreach (var item in itemsStrList)
            {
                result.Add($"{item.Replace(Environment.NewLine, "").Trim()} .");
            }

            return result;
        }
    }
}
