using CsQuery;
using FindInCn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class GearBest : IRemoteShop
    {
        public GearBest()
        {
            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Logo { get { return "http://css.gearbest.com/imagecache/GB2/images/domeimg/index/logo.png"; } }

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

                Dictionary<string, string> result = new Dictionary<string, string>();
                CQ dom = WebHelper.GetHttpResponse(CategoriesUrl);
                dom = dom["select.searchSelect"].FirstOrDefault().InnerHTML;

                foreach (var item in dom["option"])
                {
                    CQ listItem = item.InnerHTML;
                    result.Add(WebUtility.HtmlDecode(item.InnerText), item.GetAttribute("value"));
                }

                return result;
            }
        }

        string SearchUrl;

        string CategoriesUrl;

        string NotInitializedExceptionMessage = "Call Init() before use this method";

        public void Init(int id, string name, string url, string searchUrl, string categoriesUrl)
        {
            Id = id;
            Name = name;
            Url = url;
            SearchUrl = searchUrl;
            CategoriesUrl = categoriesUrl;
        }
    }
}
