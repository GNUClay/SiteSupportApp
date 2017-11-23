using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class EnumPage : BaseStructElementPage
    {
        public EnumPage(NameOfEnumNode nameNode, BaseApiPage parent)
            : base(nameNode, parent)
        {
            mNameNode = nameNode;

            mEnumInfo = new EnumInfo(nameNode.XMLDocWrapper, nameNode.FullName);
            mXMLDocWrapper = nameNode.XMLDocWrapper;
            mMemberInfo = mXMLDocWrapper.LoadMemberInfo($"T:{nameNode.FullName}");

            Title = $"GNU Clay - {mNameNode.Name} Enumeration";
        }

        private XMLDocWrapper mXMLDocWrapper;
        private NameOfEnumNode mNameNode;
        private EnumInfo mEnumInfo;

        protected override void GenerateText()
        {
            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");

            AppendLine($"<h1>{mNameNode.Name} Enumeration</h1>");
            PrintFirstText();
            PrintMembers();
            PrintLastText();

            AppendLine("<p>&nbsp;</p>");
            AppendLine("</article>");
        }

        private void PrintMembers()
        {
            if(mEnumInfo.Items.Count > 0)
            {
                AppendLine("<h3>Members</h3>");
                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Member name</th><th>Description</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");

                foreach(var memberName in mEnumInfo.Items)
                {
                    var memberInfo = mXMLDocWrapper.LoadMemberInfo(memberName);

                    var summary = memberInfo.Summaries.FirstOrDefault();

                    AppendLine("<tr>");
                    AppendLine($"<td><a name='{memberInfo.NameForHref}'></a>{memberInfo.Name}</td>");
                    AppendLine($"<td>{summary?.Content}</td>");
                    AppendLine("</tr>");
                }

                AppendLine("</tbody>");
                AppendLine("</table>");
            }
        }
    }
}
