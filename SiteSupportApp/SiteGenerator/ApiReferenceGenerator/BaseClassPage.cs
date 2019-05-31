using CommonSiteGeneratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    //public class BaseClassPage : BaseStructElementPage
    //{
    //    protected BaseClassPage(NameOfClassNode nameNode, BaseApiPage parent, BaseSiteItemsFactory factory)
    //        : base(nameNode, parent, factory)
    //    {
    //        mClassInfo = new ClassInfo(nameNode.XMLDocWrapper, nameNode.FullName, nameNode.Kind);

    //        mXMLDocWrapper = nameNode.XMLDocWrapper;
    //        mMemberInfo = mXMLDocWrapper.LoadMemberInfo($"T:{nameNode.FullName}");
    //    }

    //    private ClassInfo mClassInfo;
    //    private XMLDocWrapper mXMLDocWrapper;
        
    //    protected void PrintMembers()
    //    {
    //        if(mClassInfo.Properties.Count > 0)
    //        {
    //            AppendLine("<h3>Properties</h3>");

    //            foreach (var memberName in mClassInfo.Properties)
    //            {
    //                PrintMember(memberName, "p");
    //            }
    //        }

    //        if (mClassInfo.Methods.Count > 0)
    //        {
    //            AppendLine("<h3>Methods</h3>");

    //            foreach (var memberName in mClassInfo.Methods)
    //            {
    //                PrintMember(memberName, "m");
    //            }
    //        }

    //        if (mClassInfo.Events.Count > 0)
    //        {
    //            AppendLine("<h3>Events</h3>");

    //            foreach (var memberName in mClassInfo.Events)
    //            {
    //                PrintMember(memberName, "e");
    //            }
    //        }
    //    }

    //    private void PrintMember(string memberName, string memberPrefix)
    //    {
    //        var memberInfo = mXMLDocWrapper.LoadMemberInfo(memberName);

    //        AppendLine($"<a name='{memberPrefix}-{memberInfo.NameForHref}'></a>");
    //        AppendLine($"<h4><strong>{memberInfo.Name}</strong></h4>");
    //        PrintMemberContent(memberInfo);
    //    }
    //}
}
