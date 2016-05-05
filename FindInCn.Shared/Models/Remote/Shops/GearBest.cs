using CsQuery;
using FindInCn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class GearBest : IRemoteShop
    {
        public GearBest()
        {
            
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<IRemoteSearchItem> Search(SearchOptions options)
        {
            if (SearchUrl==null)
            {
                throw new ArgumentNullException(NotInitializedExceptionMessage);
            }

            var result = new List<IRemoteSearchItem>();

            CQ dom = WebHelper.GetHttpResponse(string.Format(SearchUrl, options.Name));
            dom = dom["div.catePro_ListBox"].FirstOrDefault().InnerHTML;

            foreach (var item in dom["li"])
            {
                CQ listItem = item.InnerHTML;
                var link = listItem["p.all_proNam a"].FirstOrDefault();
                result.Add(new GenericSearchItem<GearBest>()
                {
                    Name = link.GetAttribute("title"),
                    Url = link.GetAttribute("href"),
                    ImageUrl = listItem["img"].FirstOrDefault().GetAttribute("data-original"),
                    PriceString = listItem["span.my_shop_price"].FirstOrDefault().InnerText + "USD"
                });
            }

            return result;
        }

        public IDictionary<string, string> Categories
        {
            get
            {
                if (CategoriesUrl == null)
                {
                    throw new ArgumentNullException(NotInitializedExceptionMessage);
                }

                throw new NotImplementedException();
            }
        }

        string SearchUrl;

        string CategoriesUrl;

        string NotInitializedExceptionMessage = "Call Init() before use this method";

        public void Init(string name, string url, string searchUrl, string categoriesUrl)
        {
            Name = name;
            Url = url;
            SearchUrl = searchUrl;
            CategoriesUrl = categoriesUrl;
        }
    }
}
