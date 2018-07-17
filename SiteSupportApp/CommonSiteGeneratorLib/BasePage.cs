using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public abstract class BasePage
    {
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public System.DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public menu AdditionalMenu { get; set; }
        public bool UseMinification { get; set; } = false;
        public bool EnableMathML { get; set; }
        public abstract string TargetFileName { get; set; }

        private StringBuilder mResult;

        public virtual void Run()
        {
            mResult = new StringBuilder();
            GenerateText();

            using (var tmpTextWriter = new StreamWriter(TargetFileName, false, new UTF8Encoding(true)))
            {
                tmpTextWriter.Write(Result);
                tmpTextWriter.Flush();
            }
        }

        protected void Append(string val)
        {
            if (UseMinification)
            {
                val = val.Trim();
            }
            mResult.Append(val);
        }

        protected void AppendLine(string val)
        {
            if (UseMinification)
            {
                Append(val);
                return;
            }

            mResult.AppendLine(val);
        }

        protected abstract void GenerateText();

        public string Result
        {
            get
            {
                if (mResult == null)
                {
                    return string.Empty;
                }

                var result = mResult.ToString();

                if (UseMinification)
                {
                    //result = result.Replace(Environment.NewLine, string.Empty);
                }

                return result.Trim();
            }
        }
    }
}
