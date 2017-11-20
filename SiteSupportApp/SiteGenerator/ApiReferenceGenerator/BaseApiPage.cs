using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.ApiReferenceGenerator
{
    public class BaseApiPage : BaseTargetPage
    {
        protected BaseApiPage(BaseApiPage parent)
        {
            mParent = parent;
        }

        private BaseApiPage mParent;
        protected string Name { get; set; }

        protected List<BaseApiPage> GetNavBarList()
        {
            List<BaseApiPage> result;

            if(mParent == null)
            {
                result = new List<BaseApiPage>();
            }
            else
            {
                result = mParent.GetNavBarList();
            }

            result.Add(this);

            return result;
        }

        protected void GenerateNavBar()
        {
            var targetList = GetNavBarList();

            var lastItem = targetList.Last();

            AppendLine("<nav aria-label='breadcrumb' role='navigation'>");
            AppendLine("<ol class='breadcrumb'>");

            foreach (var item in targetList)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"GenerateNavBar item.RelativeHref = {item.RelativeHref} item.Name = {item.Name}");

                if(item == lastItem)
                {
                    AppendLine($"<li class='breadcrumb-item active' aria-current='page'>{item.Name}</li>");
                }
                else
                {
                    AppendLine($"<li class='breadcrumb-item' aria-current='page'><a href='{item.RelativeHref}'>{item.Name}</a></li>");
                }
                
            }
            AppendLine("</ol>");
            AppendLine("</nav>");
        }
    }
}
