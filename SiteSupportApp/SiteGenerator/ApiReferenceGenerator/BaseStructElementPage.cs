using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class BaseStructElementPage : BaseTargetPage
    {
        public BaseStructElementPage(AbstractNameNode nameNode)
        {
            mNameNode = nameNode;

            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, $"{mNameNode.FullName.ToLower()}.html");

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor TargetFileName = {TargetFileName}");
        }

        private AbstractNameNode mNameNode;
    }
}
