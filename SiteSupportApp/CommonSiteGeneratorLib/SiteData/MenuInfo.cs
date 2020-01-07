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

namespace CommonSiteGeneratorLib.SiteData
{
    public class MenuItem
    {
        public string Href { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

    }

    public class MenuInfo
    {
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        public static MenuInfo GetMenu(string menuName)
        {
            if (mMenuDict.ContainsKey(menuName))
            {
                return mMenuDict[menuName];
            }

            var menu = LoadFromFile(Path.Combine(GeneralSettings.SourcePath, menuName));
            mMenuDict[menuName] = menu;
            return menu;
        }

        private static Dictionary<string, MenuInfo> mMenuDict = new Dictionary<string, MenuInfo>();

        public static MenuInfo LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<MenuInfo>(File.ReadAllText(path));
        }
    }
}
