using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class BaseClassPage : BaseStructElementPage
    {
        protected BaseClassPage(NameOfClassNode nameNode, BaseApiPage parent)
            : base(nameNode, parent)
        {
            mClassInfo = new ClassInfo(nameNode.XMLDocWrapper, nameNode.FullName, nameNode.Kind);

            NLog.LogManager.GetCurrentClassLogger().Info($"constructor nameNode.FullName = {nameNode.FullName}");
            mXMLDocWrapper = nameNode.XMLDocWrapper;
            mMemberInfo = mXMLDocWrapper.LoadMemberInfo($"T:{nameNode.FullName}");
        }

        private ClassInfo mClassInfo;
        private XMLDocWrapper mXMLDocWrapper;
        protected MemberInfo mMemberInfo { get; private set; }

        protected void PrintFirstText()
        {
            var summaries = mMemberInfo.Summaries;

            foreach(var summary in summaries)
            {
                if(string.IsNullOrWhiteSpace(summary.Content))
                {
                    continue;
                }

                AppendLine($"<p>{summary.Content}</p>");
            }
        }

        protected void PrintMembers()
        {
            if(mClassInfo.Properties.Count > 0)
            {
                AppendLine("<h3>Properties</h3>");

                foreach (var memberName in mClassInfo.Properties)
                {
                    PrintMember(memberName, "p");
                }
            }

            if (mClassInfo.Methods.Count > 0)
            {
                AppendLine("<h3>Methods</h3>");

                foreach (var memberName in mClassInfo.Methods)
                {
                    PrintMember(memberName, "m");
                }
            }

            if (mClassInfo.Events.Count > 0)
            {
                AppendLine("<h3>Events</h3>");

                foreach (var memberName in mClassInfo.Events)
                {
                    PrintMember(memberName, "e");
                }
            }
        }

        private void PrintMember(string memberName, string memberPrefix)
        {
            var memberInfo = mXMLDocWrapper.LoadMemberInfo(memberName);

            AppendLine($"<a name='{memberPrefix}-{memberInfo.NameForHref}'></a>");
            AppendLine($"<h4><strong>{memberInfo.Name}</strong></h4>");
            PrintMemberContent(memberInfo);
        }

        private void PrintMemberContent(MemberInfo memberInfo)
        {
            var summaries = memberInfo.Summaries;

            foreach (var summary in summaries)
            {
                if (string.IsNullOrWhiteSpace(summary.Content))
                {
                    continue;
                }

                AppendLine($"<p>{summary.Content}</p>");
            }

            var typeParams = memberInfo.TypeParams;

            if(typeParams.Count > 0)
            {
                AppendLine("<p><strong>Type parameters</strong></p>");

                AppendLine("<ul>");
                foreach (var item in typeParams)
                {
                    AppendLine($"<li type='none'><strong>{item.Name}</strong></br>");
                    AppendLine(item.Content);
                    AppendLine("</li>");
                }
                AppendLine("</ul>");
            }

            var paramsList = memberInfo.Params;

            if (paramsList.Count > 0)
            {
                AppendLine("<p><strong>Parameters</strong></p>");

                AppendLine("<ul>");
                foreach (var item in paramsList)
                {
                    AppendLine($"<li type='none'><strong>{item.Name}</strong></br>");
                    AppendLine(item.Content);
                    AppendLine("</li>");
                }
                AppendLine("</ul>");
            }

            var returnValue = memberInfo.Returns.FirstOrDefault();

            if (returnValue != null)
            {
                AppendLine("<p><strong>Return Value</strong></p>");
                AppendLine($"<p>{returnValue.Content}</p>");
            }

            var value = memberInfo.Values.FirstOrDefault();

            if (value != null)
            {
                AppendLine("<p><strong>Value</strong></p>");
                AppendLine($"<p>{value.Content}</p>");
            }

            var para = memberInfo.Para;

            if (para.Count > 0)
            {
                foreach (var item in para)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }

            var exceptions = memberInfo.Exceptions;

            if (exceptions.Count > 0)
            {
                AppendLine("<p><strong>Exceptions</strong></p>");
                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Exception</th><th>Condition</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in exceptions)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td>{item.Name}</td>");
                    AppendLine($"<td>{item.Content}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }

            var examples = memberInfo.Examples;

            if (examples.Count > 0)
            {
                AppendLine("<p><strong>Examples</strong></p>");

                foreach (var item in examples)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }

            var remarks = memberInfo.Remarks;

            if (remarks.Count > 0)
            {
                AppendLine("<p><strong>Remarks</strong></p>");

                foreach (var item in remarks)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }
        }

        protected void PrintLastText()
        {
            var para = mMemberInfo.Para;

            if (para.Count > 0)
            {
                foreach (var item in para)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }

            var exceptions = mMemberInfo.Exceptions;

            if (exceptions.Count > 0)
            {
                AppendLine("<h3>Exceptions</h3>");
                AppendLine("<table class='table table-bordered'>");
                AppendLine("<thead>");
                AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Exception</th><th>Condition</th></tr>");
                AppendLine("</thead>");
                AppendLine("<tbody>");
                foreach (var item in exceptions)
                {
                    AppendLine("<tr>");
                    AppendLine($"<td>{item.Name}</td>");
                    AppendLine($"<td>{item.Content}</td>");
                    AppendLine("</tr>");
                }
                AppendLine("</tbody>");
                AppendLine("</table>");
            }

            var examples = mMemberInfo.Examples;

            if (examples.Count > 0)
            {
                AppendLine("<h3>Examples</h3>");

                foreach (var item in examples)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }

            var remarks = mMemberInfo.Remarks;

            if (remarks.Count > 0)
            {
                AppendLine("<h3>Remarks</h3>");

                foreach (var item in remarks)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        continue;
                    }

                    AppendLine($"<p>{item.Content}</p>");
                }
            }
        }
    }
}
