using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class SplitedNamesResult
    {
        public Dictionary<string, List<string>> NamesWithTailes { get; set; } = new Dictionary<string, List<string>>();
        public List<string> WithoutTailes { get; set; } = new List<string>();
    }

    public abstract class AbstractNameNode
    {
        protected AbstractNameNode(XMLDocWrapper doc, AbstractNameNode parent, string name)
        {
            mDoc = doc;
            mParent = parent;
            Name = name;

            //NLog.LogManager.GetCurrentClassLogger().Info($"constructor Name = {Name}");

            if (mParent == null)
            {
                FullName = Name;
            }
            else
            { 
                var parentFullName = mParent.FullName;

                //NLog.LogManager.GetCurrentClassLogger().Info($"constructor parentFullName = {parentFullName}");

                if(!string.IsNullOrWhiteSpace(parentFullName))
                {
                    FullName = $"{parentFullName}.{Name}";
                }
                else
                {
                    FullName = Name;
                }
            }

            //NLog.LogManager.GetCurrentClassLogger().Info($"constructor FullName = {FullName}");
        }

        protected XMLDocWrapper mDoc;
        protected AbstractNameNode mParent;

        public string Name { get; private set; } = string.Empty;
        public string FullName { get; private set; }
        public abstract string KindName { get; }

        public List<NameOfClassNode> Classes { get; set; } = new List<NameOfClassNode>();
        public List<NameOfClassNode> Interfaces { get; set; } = new List<NameOfClassNode>();
        public List<NameOfClassNode> Structs { get; set; } = new List<NameOfClassNode>();
        public List<NameOfDelegateNode> Delegates { get; set; } = new List<NameOfDelegateNode>();
        public List<NameOfEnumNode> Enums { get; set; } = new List<NameOfEnumNode>();

        protected static SplitedNamesResult GetSplitedNames(List<string> initList)
        {
            var result = new SplitedNamesResult();

            var dict = result.NamesWithTailes;

            foreach (var typeName in initList)
            {
                var posOfPoint = typeName.IndexOf(".");

                if (posOfPoint > 0)
                {
                    var name = typeName.Substring(0, posOfPoint);
                    var tail = typeName.Substring(posOfPoint + 1, typeName.Length - posOfPoint - 1);

                    List<string> listOfTailes = null;

                    if (dict.ContainsKey(name))
                    {
                        listOfTailes = dict[name];
                    }
                    else
                    {
                        listOfTailes = new List<string>();
                        dict[name] = listOfTailes;
                    }

                    listOfTailes.Add(tail);
                }
                else
                {
                    result.WithoutTailes.Add(typeName);
                }
            }

            return result;
        }

        protected void ProcessWithoutTailes(List<string> targetNames)
        {
            foreach (var typeName in targetNames)
            {
                //NLog.LogManager.GetCurrentClassLogger().Info($"FillChildren Process typeName = {typeName}");

                var fullTypeName = $"{FullName}.{typeName}";

                //NLog.LogManager.GetCurrentClassLogger().Info($"FillChildren Process fullTypeName = {fullTypeName}");

                var summaryType = mDoc.GetSummaryType(fullTypeName);

                //NLog.LogManager.GetCurrentClassLogger().Info($"FillChildren Process summaryType = {summaryType}");

                if (string.IsNullOrWhiteSpace(summaryType))
                {
                    var classNode = new NameOfClassNode(mDoc, this, typeName, KindOfClass.Class, new List<string>());
                    Classes.Add(classNode);
                }
                else
                {
                    if (summaryType == "i")
                    {
                        var interfaceNode = new NameOfClassNode(mDoc, this, typeName, KindOfClass.Interface, new List<string>());
                        Interfaces.Add(interfaceNode);
                    }
                    else
                    {
                        if (summaryType == "d")
                        {
                            var delegateNode = new NameOfDelegateNode(mDoc, this, typeName);
                            Delegates.Add(delegateNode);
                        }
                        else
                        {
                            if (summaryType == "e")
                            {
                                var enumNode = new NameOfEnumNode(mDoc, this, typeName);
                                Enums.Add(enumNode);
                            }
                            else
                            {
                                if (summaryType == "s")
                                {
                                    var classNode = new NameOfClassNode(mDoc, this, typeName, KindOfClass.Struct, new List<string>());
                                    Structs.Add(classNode);
                                }
                                else
                                {
                                    var classNode = new NameOfClassNode(mDoc, this, typeName, KindOfClass.Class, new List<string>());
                                    Classes.Add(classNode);
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual string DisplayHierarchy(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            return $"{spaces}{KindName}:{Name}{Environment.NewLine}";
        }
    }
}
