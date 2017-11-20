using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class DllPage : BaseTargetPage
    {
        public DllPage(string configName)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"constructor configName = {configName}");

            var configFullPath = Path.Combine(GeneralSettings.ApiReferenceSourcePath, configName);

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor configFullPath = {configFullPath}");

            mApiProject = ApiProject.LoadFromFile(configFullPath);

            var targetTypes = mApiProject.targetItems;

            if(mApiProject.useAllItems)
            {
                targetTypes = new List<string>();
            }

            mXMLDocWrapper = new XMLDocWrapper(Path.Combine(GeneralSettings.ApiReferenceSourcePath, mApiProject.fileName), targetTypes);

            mAsseblyName = mXMLDocWrapper.AssemblyName();

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor mAsseblyName = {mAsseblyName}");

            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, $"{mAsseblyName.ToLower()}.html");

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor TargetFileName = {TargetFileName}");

            mSimpleRoot = mXMLDocWrapper.LoadTreeOfTypes().GetNotSimpleNamespace();
        }

        private ApiProject mApiProject;
        private XMLDocWrapper mXMLDocWrapper;
        private string mAsseblyName;
        private NameOfNamespaceNode mSimpleRoot;

        protected override void GenerateText()
        {
            foreach(var item in mSimpleRoot.Namespaces)
            {
                var tmpPage = new NamespacePage(item);
                tmpPage.Run();
            }

            foreach(var item in mSimpleRoot.Classes)
            {
                var tmpPage = new ClassPage(item);
                tmpPage.Run();
            }

            foreach (var item in mSimpleRoot.Interfaces)
            {
                var tmpPage = new InterfacePage(item);
                tmpPage.Run();
            }

            foreach (var item in mSimpleRoot.Structs)
            {
                var tmpPage = new StructPage(item);
                tmpPage.Run();
            }

            foreach (var item in mSimpleRoot.Enums)
            {
                var tmpPage = new EnumPage(item);
                tmpPage.Run();
            }

            //foreach (var item in mSimpleRoot.Delegates)
            //{
            //    var tmpPage = new DelegatePage(item);
            //    tmpPage.Run();
            //}

            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            AppendLine("<article>");
            AppendLine($"<p>{mAsseblyName}</p>");


            AppendLine("</article>");
        }
    }
}
