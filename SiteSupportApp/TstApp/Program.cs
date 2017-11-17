/* SiteSupportApp supports generating the web site <http://gnuclay.github.io>
*  Copyright (c) 2016 metatypeman
*  <https://github.com/GNUClay/SiteSupportApp.git>
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using CommonUtils;
using HtmlAgilityPack;
using SiteGenerator;
using SiteGenerator.ApiReferenceGenerator;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TSTLoadDocumentation();
            //GetPath();
            //TstVersion();
        }

        private static void TSTLoadDocumentation()
        {
            var sourcePath = GeneralSettings.SourcePath;

            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation sourcePath = {sourcePath}");

            //var path = EVPath.Normalize(@"%USERPROFILE%\Documents\GitHub\gnuclay.github.io\siteSource\api\GnuClay.LocalHost.xml");
            //var path = EVPath.Normalize(@"%USERPROFILE%\Documents\GitHub\gnuclay.github.io\siteSource\api\GnuClay.CommonClientTypes.xml");
            var path = EVPath.Normalize("TstApp.xml");

            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation path = {path}");

            var xmlDocWrapper = new XMLDocWrapper(path);

            var tree = xmlDocWrapper.LoadTreeOfTypes();

            var treeAsString = tree.DisplayHierarchy();
            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation treeAsString = {treeAsString}");

            var classInfo = new ClassInfo(xmlDocWrapper, "T:TstApp.ExampleNamespace.Example2", KindOfClass.Class);
            var classInfoAsString = classInfo.DisplayMembers();
            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation classInfoAsString = {classInfoAsString}");

            var enumInfo = new EnumInfo(xmlDocWrapper, "T:TstApp.ExampleClass.ExampleEnum");
            var enumInfoAsString = enumInfo.DisplayItems();
            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation enumInfoAsString = {enumInfoAsString}");

            var memberInfo = xmlDocWrapper.LoadMemberInfo("P:TstApp.ExampleNamespace.Example2.SomeProp");
            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation memberInfo = {memberInfo}");

            memberInfo = xmlDocWrapper.LoadMemberInfo("M:TstApp.ExampleNamespace.Example2.Run2``1(System.Int32)");
            NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation memberInfo = {memberInfo}");

            //using (var fs = File.OpenRead(path))
            //{
            //    var doc = XDocument.Load(fs);

            //    var members = doc.Root.Elements().Where(p => p.Name.LocalName.ToLower() == "members").Elements().ToList();

            //    foreach(var item in members)
            //    {
            //        NLog.LogManager.GetCurrentClassLogger().Info($"TSTLoadDocumentation item = {item.ToString()}");
            //    }
            //}
        }

        private static void GetPath()
        {
            foreach(DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"GetPath item.Key = {item.Key} item.Value = {item.Value}");
            }

            var sourcePath = "%USERPROFILE%/Source";

            //var sourcePath = GeneralSettings.SourcePath;

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath sourcePath = {sourcePath}");

            var normalizedPath = EVPath.Normalize(sourcePath);

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath normalizedPath = {normalizedPath}");

            sourcePath = "%CommonProgramFiles(x86)%/Source";

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath sourcePath = {sourcePath}");

            normalizedPath = EVPath.Normalize(sourcePath);

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath normalizedPath = {normalizedPath}");

            sourcePath = "c:/users//Source";

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath sourcePath = {sourcePath}");
            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath Path.GetDirectoryName(sourcePath) = {Path.GetFullPath(sourcePath)}");
        }

        private static void TstVersion()
        {
            var content = "[assembly: AssemblyFileVersion(\"1.1.0.0\")]";

            var match = Regex.Match(content, "AssemblyFileVersion\\(\"\\d+.\\d+.\\d+.\\d+\"\\)");

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath match = {match}");

            var version = VersionWorker.GetVersion(@"c:\Users\Сергей\Documents\GitHub\SiteSupportApp\SiteSupportApp\TstApp");

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath version = {version}");

            var shortVersion = VersionWorker.GetShortVersion(version);

            NLog.LogManager.GetCurrentClassLogger().Info($"GetPath shortVersion = {shortVersion}");
        }
    }
}
