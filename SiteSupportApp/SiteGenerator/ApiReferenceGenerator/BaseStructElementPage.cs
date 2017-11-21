using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class BaseStructElementPage : BaseApiPage
    {
        public BaseStructElementPage(AbstractNameNode nameNode, BaseApiPage parent)
            : base(parent)
        {
            mNameNode = nameNode;

            TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, $"{mNameNode.FullName.ToLower()}.html");

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor TargetFileName = {TargetFileName}");

            Name = mNameNode.Name;
        }

        private AbstractNameNode mNameNode;

        public AbstractNameNode NameNode
        {
            get
            {
                return mNameNode;
            }
        }

        protected string GetSummary(string typeName)
        {
            NLog.LogManager.GetCurrentClassLogger().Info($"GetSummary typeName = {typeName}");

            if(!typeName.StartsWith("T:"))
            {
                typeName = $"T:{typeName}";
            }

            var info = mNameNode.XMLDocWrapper.LoadMemberInfo(typeName);

            if(info == null)
            {
                return string.Empty;
            }

            return info.Summaries.FirstOrDefault()?.Content;
        }

        private int mNameTdWith = 250;

        protected void PrintClassesList(List<ClassPage> items)
        {
            if (items.Count > 0)
            {
                AppendLine("<h3>Classes</h3>");

                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in items)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
                    AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }
        }

        protected void PrintInterfacesList(List<InterfacePage> items)
        {
            if (items.Count > 0)
            {
                AppendLine("<h3>Interfaces</h3>");

                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in items)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
                    AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }
        }

        protected void PrintStructsList(List<StructPage> items)
        {
            if (items.Count > 0)
            {
                AppendLine("<h3>Structures</h3>");

                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in items)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
                    AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }
        }

        protected void PrintEnumsList(List<EnumPage> items)
        {
            if (items.Count > 0)
            {
                AppendLine("<h3>Enums</h3>");

                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in items)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
                    AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }
        }

        protected void PrintDelegates(List<NameOfDelegateNode> items)
        {
            if (items.Count > 0)
            {
                AppendLine("<h3>Delegates</h3>");

                foreach (var item in items)
                {
                }
            }
        }

        protected void PrintMethods()
        {

        }
    }
}
