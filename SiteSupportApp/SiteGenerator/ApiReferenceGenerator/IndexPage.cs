using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class IndexPage : BaseTargetPage
    {
        public IndexPage()
        {
            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, "index.html");

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor TargetFileName = {TargetFileName}");
        }

        protected override void GenerateText()
        {
            mApiSolution = ApiSolution.LoadFromFile(GeneralSettings.ApiReferenceConfigPath);
            foreach (var item in mApiSolution.items)
            {
                var tmpDllPage = new DllPage(item);
                tmpDllPage.Run();
            }
            base.GenerateText();
        }

        private ApiSolution mApiSolution;

        protected override void GenerateArticle()
        {
            AppendLine("<article>");
            


            AppendLine("</article>");
        }
    }
}
