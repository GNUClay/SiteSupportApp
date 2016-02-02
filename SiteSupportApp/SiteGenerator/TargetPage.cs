/* SiteSupportApp supports generating the web site <http://gnuclay.github.io>
*  Copyright (c) 2016 metatypeman
*  <https://github.com/GNUClay/SiteSupportApp.git>
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.IO;
using System.Text;

namespace SiteGenerator
{
    public class TargetPage
    {
        private string mTitle = string.Empty;

        public string Title
        {
            get
            {
                return mTitle;
            }

            set
            {
                mTitle = value;
            }
        }

        private string mContent = string.Empty;

        public string Content
        {
            get
            {
                return mContent;
            }

            set
            {
                mContent = value;
            }
        }

        private StringBuilder mResult = null;

        public void Run()
        {
            mResult = new StringBuilder();

            NLog.LogManager.GetCurrentClassLogger().Info("Run");

            GenerateText();
        }

        public void Run(string targetFileName)
        {
            Run();

            NLog.LogManager.GetCurrentClassLogger().Info("Run targetFileName = {0}", targetFileName);

            using (var tmpTextWriter = new StreamWriter(targetFileName))
            {
                tmpTextWriter.Write(Result);

                tmpTextWriter.Flush();
            }
        }

        protected void Append(string val)
        {
            mResult.Append(val);
        }

        protected void AppendLine(string val)
        {
            mResult.AppendLine(val);
        }

        private void GenerateText()
        {
            AppendLine("<!DOCTYPE html>");
            AppendLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            AppendLine("    <head>");
            AppendLine("        <meta charset=\"utf-8\" />");

            if (!string.IsNullOrWhiteSpace(Title))
            {
                Append("        <title>");
                Append(Title);
                AppendLine("</title>");
            }

            AppendLine("    </head>");
            AppendLine("    <body>");
            AppendLine("        <header>");
            //AppendLine(mCommonPageSupport.Header);
            AppendLine("        </header>");

            AppendLine("        <nav>");
            //AppendLine(mCommonPageSupport.Menu);
            AppendLine("        </nav>");

            AppendLine("<hr>");

            AppendLine("        <article>");

            //if (!string.IsNullOrWhiteSpace(TextTitle))
            //{
            //    Append("            <h1>");
            //    AppendLine(TextTitle);
            //    AppendLine("</h1>");
            //}

            AppendLine(Content);

            AppendLine("        </article>");

            AppendLine("<hr>");

            AppendLine("        <footer>");
            //AppendLine(mCommonPageSupport.Footer);
            AppendLine("        </footer>");
            AppendLine("    </body>");
            AppendLine("</html>");
        }

        public string Result
        {
            get
            {
                if (mResult == null)
                {
                    return string.Empty;
                }

                return mResult.ToString();
            }
        }
    }
}
