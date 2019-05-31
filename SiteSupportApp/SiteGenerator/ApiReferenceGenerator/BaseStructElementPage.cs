using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    //public class BaseStructElementPage : BaseApiPage
    //{
    //    public BaseStructElementPage(AbstractNameNode nameNode, BaseApiPage parent, BaseSiteItemsFactory factory)
    //        : base(parent, factory)
    //    {
    //        mNameNode = nameNode;

    //        TargetFileName = Path.Combine(GeneralSettings.ApiReferenceTargetPath, $"{mNameNode.FullName.ToLower()}.html");

    //        Name = mNameNode.Name;
    //    }

    //    private AbstractNameNode mNameNode;

    //    public AbstractNameNode NameNode
    //    {
    //        get
    //        {
    //            return mNameNode;
    //        }
    //    }

    //    protected string GetSummary(string typeName)
    //    {
    //        if(!typeName.StartsWith("T:"))
    //        {
    //            typeName = $"T:{typeName}";
    //        }

    //        var info = mNameNode.XMLDocWrapper.LoadMemberInfo(typeName);

    //        if(info == null)
    //        {
    //            return string.Empty;
    //        }

    //        return info.Summaries.FirstOrDefault()?.Content;
    //    }

    //    protected int mNameTdWith = 250;

    //    protected void PrintClassesList(List<ClassPage> items)
    //    {
    //        if (items.Count > 0)
    //        {
    //            AppendLine("<h3>Classes</h3>");

    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in items)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
    //                AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }
    //    }

    //    protected void PrintInterfacesList(List<InterfacePage> items)
    //    {
    //        if (items.Count > 0)
    //        {
    //            AppendLine("<h3>Interfaces</h3>");

    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in items)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
    //                AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }
    //    }

    //    protected void PrintStructsList(List<StructPage> items)
    //    {
    //        if (items.Count > 0)
    //        {
    //            AppendLine("<h3>Structures</h3>");

    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in items)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
    //                AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }
    //    }

    //    protected void PrintEnumsList(List<EnumPage> items)
    //    {
    //        if (items.Count > 0)
    //        {
    //            AppendLine("<h3>Enums</h3>");

    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Name</th><th>Description</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in items)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td><a href='{item.RelativeHref}'>{item.Name}</a></td>");
    //                AppendLine($"<td>{GetSummary(item.NameNode.FullName)}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }
    //    }

    //    protected void PrintDelegates(List<NameOfDelegateNode> items)
    //    {
    //        if (items.Count > 0)
    //        {
    //            AppendLine("<h3>Delegates</h3>");

    //            foreach (var item in items)
    //            {
    //                var memberInfo = mNameNode.XMLDocWrapper.LoadMemberInfo($"T:{item.FullName}");

    //                AppendLine($"<a name='d-{memberInfo.NameForHref}'></a>");
    //                AppendLine($"<h4><strong>{memberInfo.Name} Delegate</strong></h4>");
    //                PrintMemberContent(memberInfo);
    //            }
    //        }
    //    }

    //    protected void PrintMemberContent(MemberInfo memberInfo)
    //    {
    //        var summaries = memberInfo.Summaries;

    //        foreach (var summary in summaries)
    //        {
    //            if (string.IsNullOrWhiteSpace(summary.Content))
    //            {
    //                continue;
    //            }

    //            AppendLine($"<p>{summary.Content}</p>");
    //        }

    //        var typeParams = memberInfo.TypeParams;

    //        if (typeParams.Count > 0)
    //        {
    //            AppendLine("<p><strong>Type parameters</strong></p>");

    //            AppendLine("<ul>");
    //            foreach (var item in typeParams)
    //            {
    //                AppendLine($"<li type='none'><strong>{item.Name}</strong></br>");
    //                AppendLine(item.Content);
    //                AppendLine("</li>");
    //            }
    //            AppendLine("</ul>");
    //        }

    //        var paramsList = memberInfo.Params;

    //        if (paramsList.Count > 0)
    //        {
    //            AppendLine("<p><strong>Parameters</strong></p>");

    //            AppendLine("<ul>");
    //            foreach (var item in paramsList)
    //            {
    //                AppendLine($"<li type='none'><strong>{item.Name}</strong></br>");
    //                AppendLine(item.Content);
    //                AppendLine("</li>");
    //            }
    //            AppendLine("</ul>");
    //        }

    //        var returnValue = memberInfo.Returns.FirstOrDefault();

    //        if (returnValue != null)
    //        {
    //            AppendLine("<p><strong>Return Value</strong></p>");
    //            AppendLine($"<p>{returnValue.Content}</p>");
    //        }

    //        var value = memberInfo.Values.FirstOrDefault();

    //        if (value != null)
    //        {
    //            AppendLine("<p><strong>Value</strong></p>");
    //            AppendLine($"<p>{value.Content}</p>");
    //        }

    //        var para = memberInfo.Para;

    //        if (para.Count > 0)
    //        {
    //            foreach (var item in para)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }

    //        var exceptions = memberInfo.Exceptions;

    //        if (exceptions.Count > 0)
    //        {
    //            AppendLine("<p><strong>Exceptions</strong></p>");
    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Exception</th><th>Condition</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in exceptions)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td>{item.Name}</td>");
    //                AppendLine($"<td>{item.Content}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }

    //        var examples = memberInfo.Examples;

    //        if (examples.Count > 0)
    //        {
    //            AppendLine("<p><strong>Examples</strong></p>");

    //            foreach (var item in examples)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }

    //        var remarks = memberInfo.Remarks;

    //        if (remarks.Count > 0)
    //        {
    //            AppendLine("<p><strong>Remarks</strong></p>");

    //            foreach (var item in remarks)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }
    //    }

    //    protected MemberInfo mMemberInfo { get; set; }

    //    protected void PrintFirstText()
    //    {
    //        var summaries = mMemberInfo.Summaries;

    //        foreach (var summary in summaries)
    //        {
    //            if (string.IsNullOrWhiteSpace(summary.Content))
    //            {
    //                continue;
    //            }

    //            AppendLine($"<p>{summary.Content}</p>");
    //        }
    //    }

    //    protected void PrintLastText()
    //    {
    //        var para = mMemberInfo.Para;

    //        if (para.Count > 0)
    //        {
    //            foreach (var item in para)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }

    //        var exceptions = mMemberInfo.Exceptions;

    //        if (exceptions.Count > 0)
    //        {
    //            AppendLine("<h3>Exceptions</h3>");
    //            AppendLine("<table class='table table-bordered'>");
    //            AppendLine("<thead>");
    //            AppendLine($"<tr><th style='width:{mNameTdWith}px;'>Exception</th><th>Condition</th></tr>");
    //            AppendLine("</thead>");
    //            AppendLine("<tbody>");
    //            foreach (var item in exceptions)
    //            {
    //                AppendLine("<tr>");
    //                AppendLine($"<td>{item.Name}</td>");
    //                AppendLine($"<td>{item.Content}</td>");
    //                AppendLine("</tr>");
    //            }
    //            AppendLine("</tbody>");
    //            AppendLine("</table>");
    //        }

    //        var examples = mMemberInfo.Examples;

    //        if (examples.Count > 0)
    //        {
    //            AppendLine("<h3>Examples</h3>");

    //            foreach (var item in examples)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }

    //        var remarks = mMemberInfo.Remarks;

    //        if (remarks.Count > 0)
    //        {
    //            AppendLine("<h3>Remarks</h3>");

    //            foreach (var item in remarks)
    //            {
    //                if (string.IsNullOrWhiteSpace(item.Content))
    //                {
    //                    continue;
    //                }

    //                AppendLine($"<p>{item.Content}</p>");
    //            }
    //        }
    //    }
    //}
}
