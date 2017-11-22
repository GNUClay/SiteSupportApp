﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class StructPage : BaseClassPage
    {
        public StructPage(NameOfClassNode nameNode, BaseApiPage parent)
            : base(nameNode, parent)
        {
            mNameNode = nameNode;
        }

        private NameOfClassNode mNameNode;

        protected override void GenerateText()
        {
            base.GenerateText();
        }

        protected override void GenerateArticle()
        {
            GenerateNavBar();

            AppendLine("<article>");
            AppendLine($"<h1>{mNameNode.Name} Structure</h1>");
            PrintFirstText();
            PrintMembers();
            PrintLastText();

            AppendLine("<p>&nbsp;</p>");
            AppendLine("</article>");
        }
    }
}
