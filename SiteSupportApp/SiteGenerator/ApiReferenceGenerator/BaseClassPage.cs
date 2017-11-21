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
                    PrintMember(memberName);
                }
            }

            if (mClassInfo.Methods.Count > 0)
            {
                AppendLine("<h3>Methods</h3>");

                foreach (var memberName in mClassInfo.Methods)
                {
                    PrintMember(memberName);
                }
            }

            if (mClassInfo.Events.Count > 0)
            {
                AppendLine("<h3></h3>");

                foreach (var memberName in mClassInfo.Events)
                {
                    PrintMember(memberName);
                }
            }
        }

        private void PrintMember(string memberName)
        {
            var memberInfo = mXMLDocWrapper.LoadMemberInfo(memberName);


        }
    }
}
