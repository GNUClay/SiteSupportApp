using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSiteGeneratorLib.SiteData
{
    public class SitePageMicroDataInfo
    {
        /// <summary>
        /// og:description, description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// og:image:secure_url, image
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// og:image:alt
        /// </summary>
        public string ImageAlt { get; set; } = string.Empty;

        /// <summary>
        /// og:title. If this title is empty I will take title of the page.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
