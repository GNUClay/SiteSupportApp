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

using System.IO;
using System.Xml;


namespace SiteGenerator.DocBookProcessor.DbkToHtml
{
    public class SimpleProcessorContext
    {
        public XmlNode CurrentNode = null;
        public int Level = 0;
        public XmlNode CurrentResultNode = null;
    }

    public class SimpleProcessor
    {
        public SimpleProcessor(TextReader txtReader)
        {
            mTextReader = txtReader;
        }

        private TextReader mTextReader = null;

        private XmlDocument mXmlDocument = null;

        private XmlDocument mResultXmLDocument = null;

        public XmlDocument ResultDocument
        {
            get
            {
                return mResultXmLDocument;
            }
        }

        public void Run()
        {
            PreparareXMLDocument();

            NLog.LogManager.GetCurrentClassLogger().Info("Run");

            var tmpIterator = mXmlDocument.ChildNodes.GetEnumerator();

            tmpIterator.MoveNext();

            tmpIterator.MoveNext();

            ProcessingRootNode((XmlNode)tmpIterator.Current);
        }

        private void PreparareXMLDocument()
        {
            mXmlDocument = new XmlDocument();

            mXmlDocument.Load(mTextReader);

            mResultXmLDocument = new XmlDocument();

            mResultXmLDocument.AppendChild(mResultXmLDocument.CreateElement("t"));
        }

        private void ProcessingRootNode(XmlNode node)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(node.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(node.InnerXml);

            var tmpContext = new SimpleProcessorContext();

            tmpContext.CurrentNode = node;

            tmpContext.CurrentResultNode = mResultXmLDocument.DocumentElement;

            DispatchNode(tmpContext);
        }

        private void DispatchNode(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("DispatchNode");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);

            if (context.CurrentNode.Name == "article")
            {
                ProcessArticleNode(context);

                return;
            }

            if (context.CurrentNode.Name == "title")
            {
                ProcessTitle(context);

                return;
            }

            if (context.CurrentNode.Name == "para")
            {
                ProcessPara(context);

                return;
            }

            if (context.CurrentNode.Name == "section")
            {
                ProcessSection(context);

                return;
            }

            if (context.CurrentNode.Name == "itemizedlist")
            {
                ProcessItemizedList(context);

                return;
            }

            if (context.CurrentNode.Name == "orderedlist")
            {
                ProcessOrderedList(context);

                return;
            }

            if (context.CurrentNode.Name == "listitem")
            {
                ProcessListItem(context);

                return;
            }
        }

        private void ProcessArticleNode(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessArticleNode");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);

            ProcessChilds(context);
        }

        private void ProcessChilds(SimpleProcessorContext context)
        {
            context.Level++;

            var tmpCurrNode = context.CurrentNode;

            foreach (XmlNode child in context.CurrentNode.ChildNodes)
            {
                context.CurrentNode = child;

                DispatchNode(context);
            }

            context.CurrentNode = tmpCurrNode;

            context.Level--;
        }

        private void ProcessTitle(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessTitle");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            NLog.LogManager.GetCurrentClassLogger().Info("context.Level = {0}", context.Level);

            var tmpTargetLevel = context.Level;

            if(tmpTargetLevel > 6)
            {
                tmpTargetLevel = 6;
            }

            var tmpElem = mResultXmLDocument.CreateElement("h" + tmpTargetLevel.ToString());
            tmpElem.InnerText = context.CurrentNode.InnerText.Trim();

            context.CurrentResultNode.AppendChild(tmpElem);
        }

        private void ProcessPara(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessPara");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            var tmpElem = mResultXmLDocument.CreateElement("p");
            tmpElem.InnerText = context.CurrentNode.InnerText.Trim();

            context.CurrentResultNode.AppendChild(tmpElem);
        }

        private void ProcessSection(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessSection");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            ProcessChilds(context);
        }

        private void ProcessItemizedList(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessItemizedList");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            ProcessWithSubNodes(context, "ul");
        }

        private void ProcessOrderedList(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessOrderedList");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            ProcessWithSubNodes(context, "ol");
        }

        private void ProcessListItem(SimpleProcessorContext context)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("ProcessListItem");
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Name);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerXml);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.InnerText);
            NLog.LogManager.GetCurrentClassLogger().Info(context.CurrentNode.Value);

            ProcessWithSubNodes(context, "li");
        }

        private void ProcessWithSubNodes(SimpleProcessorContext context, string tagName)
        {
            var tmpCurrentNode = context.CurrentNode;

            var tmpCurrentResultNode = context.CurrentResultNode;

            var tmpElem = mResultXmLDocument.CreateElement(tagName);
            context.CurrentResultNode.AppendChild(tmpElem);

            context.CurrentResultNode = tmpElem;

            context.Level++;

            foreach (XmlNode child in context.CurrentNode.ChildNodes)
            {
                NLog.LogManager.GetCurrentClassLogger().Info(child.Name);

                context.CurrentNode = child;

                DispatchNode(context);
            }

            context.Level--;

            context.CurrentNode = tmpCurrentNode;

            context.CurrentResultNode = tmpCurrentResultNode;
        }
    }
}
