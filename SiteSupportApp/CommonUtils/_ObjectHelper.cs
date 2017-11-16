using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class _ObjectHelper
    {
        /// <summary>
        /// Creates string which will be contain only spaces.
        /// Count of spaces in the string equals value of parameter `indent`.
        /// </summary>
        /// <param name="indent">Count of spaces in the string.</param>
        /// <returns>String which will be contain only spaces</returns>
        public static string CreateSpaces(int indent)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < indent; i++)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}
