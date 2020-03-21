using HtmlAgilityPack;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TstApp.AddEBNFGroupsProcessing
{
    public class AddEBNFGroupsProcessor
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void NormalizeTags(string sourceFile)
        {
            _logger.Info($"sourceFile = {sourceFile}");

            var targetTags = new List<string>()
            {
                "</ContentsPlace>",
                "</LinkToContent>",
                "</EBNFCDECL>",
                "</EBNFC>",
                "</TEBNFCDECL>",
                "</GEBNFC>"
            };

            var content = File.ReadAllText(sourceFile);

            _logger.Info($"content = {content}");

            content = content.Replace("contentsplace", "ContentsPlace").Replace("linktocontent", "LinkToContent")
                .Replace("ebnfcdecl", "EBNFCDECL").Replace("ebnfc", "EBNFC").Replace("tebnfcdecl", "TEBNFCDECL").Replace("gebnfc", "GEBNFC")
                .Replace("tEBNFCDECL", "TEBNFCDECL").Replace("gEBNFC", "GEBNFC");

            _logger.Info($"content (2) = {content}");

            foreach(var targetTag in targetTags)
            {
                _logger.Info($"targetTag = '{targetTag}'");

                var pos = 0;

                while((pos = content.IndexOf(targetTag, pos)) != -1)
                {
                    _logger.Info($"pos = {pos}");

                    content = content.Remove(pos, targetTag.Length);

                    content = content.Insert(pos - 1, "/");
                }
            }

            File.WriteAllText(sourceFile, content);
        }

        public void Run(string sourceFile, string gropingEBNFName)
        {
            _logger.Info($"sourceFile = {sourceFile}");
            _logger.Info($"gropingEBNFName = {gropingEBNFName}");

            var doc = new HtmlDocument();
            doc.Load(sourceFile);

            var targetNamesList = GetNamesForGroupping(doc.DocumentNode, gropingEBNFName);

            _logger.Info($" = {targetNamesList.Count}");

            if (targetNamesList.Any())
            {
                ImplementGrouping(doc.DocumentNode, targetNamesList, gropingEBNFName);

                doc.Save(sourceFile);
            }

            _logger.Info("End");
        }

        private void ImplementGrouping(HtmlNode rootNode, List<string> targetNamesList, string gropingEBNFName)
        {
            //_logger.Info($"rootNode.Name = '{rootNode.Name}'");
            //_logger.Info($"rootNode.GetClasses() = {JsonConvert.SerializeObject(rootNode.GetClasses(), Formatting.Indented)}");
            //_logger.Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
            //_logger.Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
            //_logger.Info($"rootNode.InnerText = {rootNode.InnerText}");

            if(rootNode.Name == "ebnfcdecl" && targetNamesList.Contains(rootNode.GetAttributeValue("name", string.Empty)))
            {
                var groupStr = rootNode.GetAttributeValue("groups", string.Empty);

                //_logger.Info($"groupStr = '{groupStr}'");

                if(string.IsNullOrWhiteSpace(groupStr))
                {
                    rootNode.SetAttributeValue("groups", gropingEBNFName);
                }
                else
                {
                    if(!groupStr.Contains(gropingEBNFName))
                    {
                        groupStr = $"{groupStr};{gropingEBNFName}";

                        rootNode.SetAttributeValue("groups", groupStr);
                    }
                }
            }

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                ImplementGrouping(node, targetNamesList, gropingEBNFName);
            }
        }

        private List<string> GetNamesForGroupping(HtmlNode rootNode, string gropingEBNFName)
        {
            var htmlStrList = GetHtmlFromTargetNodes(rootNode);

            var targetFragment = SearchTargetFragment(htmlStrList, gropingEBNFName);

            _logger.Info($"targetFragment = {targetFragment}");

            var fragmentDoc = new HtmlDocument();
            fragmentDoc.LoadHtml(targetFragment);

            return fragmentDoc.DocumentNode.ChildNodes.Where(p => p.Name == "ebnfc").Select(p => p.GetAttributeValue("name", string.Empty)).Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().ToList();
        }

        private string SearchTargetFragment(List<string> htmlStrList, string gropingEBNFName)
        {
            foreach(var htmlStr in htmlStrList)
            {
                var itemsStrList = htmlStr.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                var isFragmentStarted = false;

                var buffer = new List<string>();

                foreach(var itemsStr in itemsStrList)
                {
                    if (itemsStr.Contains("ebnfcdecl"))
                    {
                        var fragmentDoc = new HtmlDocument();
                        fragmentDoc.LoadHtml(itemsStr);

                        if (fragmentDoc.DocumentNode.ChildNodes.Any(p => p.Name == "ebnfcdecl" && p.GetAttributeValue("name", string.Empty) == gropingEBNFName))
                        {
                            isFragmentStarted = true;
                            buffer.Add(itemsStr.Trim());
                        }
                    }
                    else
                    {
                        if(isFragmentStarted)
                        {
                            buffer.Add(itemsStr.Trim());
                        }
                    }

                    if(itemsStr.Contains("."))
                    {
                        isFragmentStarted = false;
                    }
                }

                if(buffer.Any())
                {
                    return string.Join(" ", buffer);
                }
            }

            return string.Empty;
        }

        private List<string> GetHtmlFromTargetNodes(HtmlNode rootNode)
        {
            var result = new List<string>();

            if (rootNode.Name == "pre" && rootNode.GetClasses().Contains("ebnf-code"))
            {
                //_logger.Info($"rootNode.Name = '{rootNode.Name}'");
                //_logger.Info($"rootNode.GetClasses() = {JsonConvert.SerializeObject(rootNode.GetClasses(), Formatting.Indented)}");
                //_logger.Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
                //_logger.Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
                //_logger.Info($"rootNode.InnerText = {rootNode.InnerText}");

                //_logger.Info("!!!!!!");

                return new List<string>() { rootNode.InnerHtml };
            }

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                result.AddRange(GetHtmlFromTargetNodes(node));
            }

            return result;
        }
    }
}
