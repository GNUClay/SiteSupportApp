using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class XMLDocWrapper
    {
        public XMLDocWrapper(string path, List<string> targetTypes)
        {
            mTargetTypes = targetTypes;

            if(mTargetTypes == null)
            {
                mTargetTypes = new List<string>();
            }

            using (var fs = File.OpenRead(path))
            {
                mDocument = XDocument.Load(fs);
                mMembersList = mDocument.Root.Elements().Where(p => p.Name.LocalName.ToLower() == "members").Elements().ToList();
            }
        }

        private XDocument mDocument;
        private List<XElement> mMembersList;
        private List<string> mTargetTypes;

        public string AssemblyName()
        {
            return mDocument.Root.Elements().Where(p => p.Name.LocalName.ToLower() == "assembly").Elements().Where(p => p.Name.LocalName.ToLower() == "name").FirstOrDefault()?.Value?.Trim();
        }

        public List<string> LoadTypeNames()
        {
            var result = new List<string>();

            foreach (var item in mMembersList)
            {
                var name = item.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value?.Trim();

                if(string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                if(!name.StartsWith("T:"))
                {
                    continue;
                }

                name = name.Replace("T:", "").Trim();

                if (mTargetTypes.Count > 0)
                {
                    if(!mTargetTypes.Contains(name))
                    {
                        continue;
                    }
                }

                result.Add(name);
            }

            result = result.Distinct().ToList();

            return result;
        }

        public NameOfNamespaceNode LoadTreeOfTypes()
        {
            var types = LoadTypeNames();

            var node = new NameOfNamespaceNode(types, null, this, null);
            return node;
        }

        public bool HasSummary(string key)
        {
            var targetMember = GetMember(key);

            if (targetMember == null)
            {
                return false;
            }

            return targetMember.Elements().Any(p => p.Name.LocalName.ToLower() == "summary");
        }

        public string GetSummaryType(string key)
        {
            var targetMember = GetMember(key);

            if(targetMember == null)
            {
                return string.Empty;
            }

            var summary = targetMember.Elements().Where(p => p.Name.LocalName.ToLower() == "summary").FirstOrDefault(p => p.Attributes().Any(x => x.Name.LocalName == "type"));

            if(summary == null)
            {
                return string.Empty;
            }

            return summary.Attributes().FirstOrDefault(p => p.Name.LocalName == "type")?.Value?.Trim();
        }

        public XElement GetMember(string key)
        {
            if(!key.StartsWith("T:"))
            {
                key = $"T:{key}";
            }

            return NGetMember(key);
        }

        private XElement NGetMember(string key)
        {
            return mMembersList.FirstOrDefault(p => p.Attributes().FirstOrDefault(x => x.Name.LocalName == "name").Value.Trim() == key);
        }

        public List<string> GetPropertiesNames(string key)
        {
            if (key.StartsWith("T:"))
            {
                key = key.Replace("T:", "");
            }

            if (!key.StartsWith("P:"))
            {
                key = $"P:{key}";
            }

            return GetMemberNames(key);
        }

        private List<string> GetMemberNames(string key)
        {
            var result = new List<string>();

            foreach (var item in mMembersList)
            {
                var name = item.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                if (!name.StartsWith(key))
                {
                    continue;
                }

                result.Add(name);
            }

            result = result.Distinct().ToList();

            return result;
        }

        public List<string> GetMethodsNames(string key)
        {
            if (key.StartsWith("T:"))
            {
                key = key.Replace("T:", "");
            }

            if (!key.StartsWith("M:"))
            {
                key = $"M:{key}";
            }

            return GetMemberNames(key);
        }

        public List<string> GetEventsNames(string key)
        {
            if (key.StartsWith("T:"))
            {
                key = key.Replace("T:", "");
            }

            if (!key.StartsWith("E:"))
            {
                key = $"E:{key}";
            }

            return GetMemberNames(key);
        }

        public List<string> GetEnumElementsNames(string key)
        {
            if (key.StartsWith("T:"))
            {
                key = key.Replace("T:", "");
            }

            if (!key.StartsWith("F:"))
            {
                key = $"F:{key}";
            }

            return GetMemberNames(key);
        }

        public MemberInfo LoadMemberInfo(string key)
        {
            var element = NGetMember(key);

            if(element == null)
            {
                return null;
            }

            var result = new MemberInfo(key);

            var summaries = element.Elements().Where(p => p.Name.LocalName.ToLower() == "summary").ToList();

            foreach(var initItem in summaries)
            {
                var item = new SummaryInfo();
                item.Content = initItem.Value?.Trim();
                result.Summaries.Add(item);
            }

            var remarks = element.Elements().Where(p => p.Name.LocalName.ToLower() == "remarks").ToList();

            foreach (var initItem in remarks)
            {
                var item = new RemarksInfo();
                item.Content = initItem.Value?.Trim();
                result.Remarks.Add(item);
            }

            var paramsList = element.Elements().Where(p => p.Name.LocalName.ToLower() == "param").ToList();

            foreach (var initItem in paramsList)
            {
                var item = new ParamInfo();
                item.Name = initItem.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value?.Trim();
                item.Content = initItem.Value?.Trim();
                result.Params.Add(item);
            }

            var values = element.Elements().Where(p => p.Name.LocalName.ToLower() == "value").ToList();

            foreach (var initItem in values)
            {
                var item = new ValueInfo();
                item.Content = initItem.Value?.Trim();
                result.Values.Add(item);
            }

            var examples = element.Elements().Where(p => p.Name.LocalName.ToLower() == "example").ToList();

            foreach (var initItem in examples)
            {
                var item = new ExampleInfo();
                item.Content = initItem.Value?.Trim();
                result.Examples.Add(item);
            }

            var exceptions = element.Elements().Where(p => p.Name.LocalName.ToLower() == "exception").ToList();

            foreach (var initItem in exceptions)
            {
                var item = new ExceptionInfo();
                item.Name = initItem.Attributes().FirstOrDefault(p => p.Name.LocalName == "cref")?.Value?.Trim();
                item.Content = initItem.Value?.Trim();
                result.Exceptions.Add(item);
            }

            var returns = element.Elements().Where(p => p.Name.LocalName.ToLower() == "returns").ToList();

            foreach (var initItem in returns)
            {
                var item = new ReturnsInfo();
                item.Content = initItem.Value?.Trim();
                result.Returns.Add(item);
            }

            var typeParams = element.Elements().Where(p => p.Name.LocalName.ToLower() == "typeparam").ToList();

            foreach (var initItem in typeParams)
            {
                var item = new TypeParamInfo();
                item.Name = initItem.Attributes().FirstOrDefault(p => p.Name.LocalName == "name")?.Value?.Trim();
                item.Content = initItem.Value?.Trim();
                result.TypeParams.Add(item);
            }

            var para = element.Elements().Where(p => p.Name.LocalName.ToLower() == "para").ToList();

            foreach (var initItem in para)
            {
                var item = new ParaInfo();
                item.Content = initItem.Value?.Trim();
                result.Para.Add(item);
            }

            return result;
        }
    }
}
