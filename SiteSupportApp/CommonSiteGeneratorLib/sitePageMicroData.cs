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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(description)} = {description}");
            sb.AppendLine($"{nameof(imageUrl)} = {imageUrl}");

            return sb.ToString();
        }
    }
}
