using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib
{
    public class sitePageMicroData
    {
        /// <summary>
        /// og:description, description
        /// </summary>
        public string description = string.Empty;

        /// <summary>
        /// og:image:secure_url, image
        /// </summary>
        public string imageUrl = string.Empty;

        /// <summary>
        /// og:image:alt
        /// </summary>
        public string imageAlt = string.Empty;

        /// <summary>
        /// og:title. If this title is empty I will take title of the page.
        /// </summary>
        public string title = string.Empty;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(description)} = {description}");
            sb.AppendLine($"{nameof(imageUrl)} = {imageUrl}");
            sb.AppendLine($"{nameof(imageAlt)} = {imageAlt}");
            sb.AppendLine($"{nameof(title)} = {title}");

            return sb.ToString();
        }
    }
}
