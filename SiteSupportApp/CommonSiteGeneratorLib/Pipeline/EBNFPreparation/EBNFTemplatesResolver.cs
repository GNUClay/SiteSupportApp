﻿using HtmlAgilityPack;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.Pipeline.EBNFPreparation
{
    public static class EBNFTemplatesResolver
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static HtmlDocument Run(HtmlDocument doc)
        {
            //_logger.Info("Begin");

            var n = 0;

            var resultFileName = Path.Combine(GeneralSettings.TempPath, $"{Guid.NewGuid().ToString("D")}.html");

            //_logger.Info($"EBNFTemplatesResolver Run resultFileName (1) = {resultFileName}");

            doc.Save(resultFileName);

            var modifiedDoc = new HtmlDocument();
            modifiedDoc.Load(resultFileName);

            var tEBNFCDECLStorage = new TEBNFCDECLStorage();

            while (RunIteration(modifiedDoc, tEBNFCDECLStorage))
            {
                n++;

                //_logger.Info($"n = {n}");

                if(n > 100)
                {
                    throw new NotSupportedException($"Too much iterations!!!");
                }

                resultFileName = Path.Combine(GeneralSettings.TempPath, $"{Guid.NewGuid().ToString("D")}.html");

                //_logger.Info($"EBNFTemplatesResolver Run resultFileName (2) = {resultFileName}");

                modifiedDoc.Save(resultFileName);

                modifiedDoc = new HtmlDocument();
                modifiedDoc.Load(resultFileName);
            };

            //_logger.Info("End");

            return modifiedDoc;
        }

        private static bool RunIteration(HtmlDocument doc, TEBNFCDECLStorage tEBNFCDECLStorage)
        {
            var gEBNFCStorage = new GEBNFCStorage();
            
            var result1 = DiscoverTemplateNodes(doc.DocumentNode, gEBNFCStorage, tEBNFCDECLStorage);
            var result2 = ExploreTemplateNodes(doc.DocumentNode, doc, gEBNFCStorage, tEBNFCDECLStorage);

            return result1 || result2;
        }

        private static bool DiscoverTemplateNodes(HtmlNode rootNode, GEBNFCStorage gEBNFCStorage, TEBNFCDECLStorage tEBNFCDECLStorage)
        {
            //if (rootNode.Name != "#document")
            //{
                //_logger.Info($"rootNode.Name = '{rootNode.Name}'");
                //_logger.Info($"rootNode.OuterHtml = {rootNode.OuterHtml}");
                //_logger.Info($"rootNode.InnerHtml = {rootNode.InnerHtml}");
                //_logger.Info($"rootNode.InnerText = {rootNode.InnerText}");
            //}

            if (rootNode.Name == "ebnfcdecl")
            {
                gEBNFCStorage.RegNode(rootNode);

                return false;
            }

            if (rootNode.Name == "tebnfcdecl")
            {
                tEBNFCDECLStorage.RegNode(rootNode);
                rootNode.Remove();

                return true;
            }

            var result = false;

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                if(DiscoverTemplateNodes(node, gEBNFCStorage, tEBNFCDECLStorage))
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool ExploreTemplateNodes(HtmlNode rootNode, HtmlDocument doc, GEBNFCStorage gEBNFCStorage, TEBNFCDECLStorage tEBNFCDECLStorage)
        {
            //if (rootNode.Name != "#document")
            //{
            //    _logger.Info($"ExploreTemplateNodes rootNode.Name = '{rootNode.Name}'");
            //    _logger.Info($"ExploreTemplateNodes rootNode.OuterHtml = {rootNode.OuterHtml}");
            //    _logger.Info($"ExploreTemplateNodes rootNode.InnerHtml = {rootNode.InnerHtml}");
            //    _logger.Info($"rootNode.InnerText = {rootNode.InnerText}");
            //}

            if (rootNode.Name == "gebnfc")
            {
                var name = rootNode.GetAttributeValue("name", string.Empty);

                //_logger.Info($"ExploreTemplateNodes name = '{name}'");

                var kind = rootNode.GetAttributeValue("kind", string.Empty);

                //_logger.Info($"ExploreTemplateNodeskind = '{kind}'");

                var itemsList = gEBNFCStorage.GetGroup(name);

                //_logger.Info($"ExploreTemplateNodesitemsList = {JsonConvert.SerializeObject(itemsList, Formatting.Indented)}");

                if (!itemsList.Any())
                {
                    rootNode.Remove();

                    return true;
                }

                var resultList = new List<string>();

                foreach (var item in itemsList)
                {
                    //_logger.Info($"item = '{item}'");

                    if (kind == "op_and")
                    {
                        resultList.Add($"[ <EBNFC name='{item}' /> ]");
                    }
                    else
                    {
                        resultList.Add($"<EBNFC name='{item}' />");
                    }
                }

                //_logger.Info($"ExploreTemplateNodes resultList = {JsonConvert.SerializeObject(resultList, Formatting.Indented)}");

                var separator = " ";

                if (kind == "or")
                {
                    separator = " | ";
                }

                var resultStr = string.Join(separator, resultList);

                //_logger.Info($"ExploreTemplateNodes resultStr = '{resultStr}'");

                var textNode = doc.CreateTextNode(resultStr);

                var parentNode = rootNode.ParentNode;
                parentNode.ReplaceChild(textNode, rootNode);

                return true;
            }

            if (rootNode.Name == "tebnfc")
            {
                tEBNFCDECLStorage.ProcessNode(rootNode, doc);

                return true;
            }

            var result = false;

            foreach (var node in rootNode.ChildNodes.ToList())
            {
                if(ExploreTemplateNodes(node, doc, gEBNFCStorage, tEBNFCDECLStorage))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
