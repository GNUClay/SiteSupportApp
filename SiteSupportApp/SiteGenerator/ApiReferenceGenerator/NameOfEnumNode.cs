﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class NameOfEnumNode: AbstractNameNode
    {
        public NameOfEnumNode(XMLDocWrapper doc, AbstractNameNode parent, string name)
            : base(doc, parent, name)
        {
        }

        public override string KindName => "enum";
    }
}
