using CommonMark;
using CommonSiteGeneratorLib.SiteData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public abstract class BasePage
    {
        protected BasePage(BaseSiteItemsFactory factory)
        {
            SiteItemsFactory = factory;
        }

        protected BaseSiteItemsFactory SiteItemsFactory { get; private set;}
        protected readonly CultureInfo TargetCulture = new CultureInfo("en-GB");
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public MenuInfo AdditionalMenu { get; set; }
        public bool UseMinification { get; set; } = false;
        public bool EnableMathML { get; set; }
        public bool UseMarkdown { get; set; }
        private string mTargetFileName;
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public string MicrodataTitle { get; set; }

        //public bool CanGenerateJsonLD
        //{
        //    get
        //    {
        //        //return 
        //    }
        //}

        public string TargetFileName
        {
            get
            {
                return mTargetFileName;
            }

            set
            {
                mTargetFileName = value;

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"TargetFileName mTargetFileName = {mTargetFileName}");
#endif

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"TargetFileName pos = {pos}");
#endif

                RelativeHref = PagesPathsHelper.PathToRelativeHref(mTargetFileName);

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"TargetFileName RelativeHref = {RelativeHref}");
#endif

                AbsoluteHref = PagesPathsHelper.RelativeHrefToAbsolute(RelativeHref);

#if DEBUG
                //NLog.LogManager.GetCurrentClassLogger().Info($"TargetFileName AbsoluteHref = {AbsoluteHref}");
#endif
            }
        }

        public string SourceName { get; set; }
        public string RelativeHref { get; private set; }
        public string AbsoluteHref { get; private set; }

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

        protected void AppendContent(string val)
        {
#if DEBUG
            //NLog.LogManager.GetCurrentClassLogger().Info($"AppendContent val = {val}");
#endif

            if(UseMarkdown)
            {
                val = CommonMarkConverter.Convert(val);
            }

            AppendLine(val);
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
