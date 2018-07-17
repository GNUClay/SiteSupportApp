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

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CommonSiteGeneratorLib
{
    public class item
    {
        public string href = string.Empty;
        public string label = string.Empty;
        public List<item> items { get; set; } = new List<item>();

    }

    public class menu
    {
        public List<item> items = new List<item>();

        public static menu GetMenu(string menuName)
        {
            if (mMenuDict.ContainsKey(menuName))
            {
                return mMenuDict[menuName];
            }

            var menu = LoadFromFile(Path.Combine(GeneralSettings.SourcePath, menuName));
            mMenuDict[menuName] = menu;
            return menu;
        }

        private static Dictionary<string, menu> mMenuDict = new Dictionary<string, menu>();

        public static menu LoadFromFile(string path)
        {
            using (var tmpfile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (tmpfile.Length == 0)
                {
                    return null;
                }

                using (var reader = new StreamReader(tmpfile))
                {
                    return JsonConvert.DeserializeObject<menu>(reader.ReadToEnd());
                }
            }
        }
    }
}
