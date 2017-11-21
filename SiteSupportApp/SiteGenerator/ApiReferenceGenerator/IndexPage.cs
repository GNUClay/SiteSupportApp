using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class IndexPage : BaseApiPage
    {
        public IndexPage()
            : base(null)
        {
            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, "index.html");

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor TargetFileName = {TargetFileName}");

            Name = "Api reference";
        }

        protected override void GenerateText()
        {
            mApiSolution = ApiSolution.LoadFromFile(GeneralSettings.ApiReferenceConfigPath);
            foreach (var item in mApiSolution.items)
            {
                var tmpDllPage = new DllPage(item, this);
                tmpDllPage.Run();
                mChildren.Add(tmpDllPage);
            }
            base.GenerateText();
        }

        private List<DllPage> mChildren = new List<DllPage>();

        private ApiSolution mApiSolution;

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");

            AppendLine("<ul>");

            foreach (var page in mChildren)
            {
                AppendLine($"<li><a href='{page.RelativeHref}'>{page.Name}</a></li>");
            }

            AppendLine("</ul>");
            AppendLine("</article>");
        }
    }
}
