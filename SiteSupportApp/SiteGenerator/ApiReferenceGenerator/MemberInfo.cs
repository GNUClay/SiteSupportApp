using CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class SummaryInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident+ 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();        
            sb.AppendLine($"{spaces}Begin Summary");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Summary");
            return sb.ToString();
        }
    }

    public class RemarksInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Remarks");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Remarks");
            return sb.ToString();
        }
    }

    public class ParamInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Param");
            sb.AppendLine($"{nextSpaces}{nameof(Name)}:'{Name}'");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Param");
            return sb.ToString();
        }
    }

    public class ValueInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Value");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Value");
            return sb.ToString();
        }
    }

    public class ExampleInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Example");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Example");
            return sb.ToString();
        }
    }

    public class ExceptionInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Exception");
            sb.AppendLine($"{nextSpaces}{nameof(Name)}:'{Name}'");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Exception");
            return sb.ToString();
        }
    }

    public class ReturnsInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Returns");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Returns");
            return sb.ToString();
        }
    }

    public class TypeParamInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin TypeParam");
            sb.AppendLine($"{nextSpaces}{nameof(Name)}:'{Name}'");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End TypeParam");
            return sb.ToString();
        }
    }

    public class ParaInfo
    {
        public string Content { get; set; } = string.Empty;

        public string ToString(int ident)
        {
            var spaces = _ObjectHelper.CreateSpaces(ident);
            var nextIdent = ident + 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}Begin Para");
            sb.AppendLine($"{nextSpaces}{nameof(Content)}:'{Content}'");
            sb.AppendLine($"{spaces}End Para");
            return sb.ToString();
        }
    }

    public class MemberInfo
    {
        public MemberInfo(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; private set; }
        public List<SummaryInfo> Summaries { get; private set; } = new List<SummaryInfo>();
        public List<RemarksInfo> Remarks { get; private set; } = new List<RemarksInfo>();
        public List<ParamInfo> Params { get; private set; } = new List<ParamInfo>();
        public List<ValueInfo> Values { get; private set; } = new List<ValueInfo>();
        public List<ExampleInfo> Examples { get; private set; } = new List<ExampleInfo>();
        public List<ExceptionInfo> Exceptions { get; private set; } = new List<ExceptionInfo>();
        public List<ReturnsInfo> Returns { get; private set; } = new List<ReturnsInfo>();
        public List<TypeParamInfo> TypeParams { get; private set; } = new List<TypeParamInfo>();
        public List<ParaInfo> Para { get; private set; } = new List<ParaInfo>();

        public override string ToString()
        {
            var nextIdent = 4;
            var nextSpaces = _ObjectHelper.CreateSpaces(nextIdent);
            var sb = new StringBuilder();
            sb.AppendLine($"Begin enum:{FullName}");
            foreach(var item in Summaries)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Remarks)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach(var item in Params)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Values)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Examples)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Exceptions)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Returns)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in TypeParams)
            {
                sb.Append(item.ToString(nextIdent));
            }
            foreach (var item in Para)
            {
                sb.Append(item.ToString(nextIdent));
            }
            sb.AppendLine($"End enum:{FullName}");
            return sb.ToString();
        }
    }
}