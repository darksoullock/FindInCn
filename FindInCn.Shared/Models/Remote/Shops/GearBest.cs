using CsQuery;
using FindInCn.Shared.Helpers;
using FindInCn.Shared.Models.DB;
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
        public GearBest(Shop info)
        {
            Init(info);
        }

        public Shop Info { get; set; }

        public IEnumerable<IRemoteSearchItem> Search(SearchOptions options)
        {
            var result = new List<IRemoteSearchItem>();

            CQ dom = WebHelper.GetHttpResponse(string.Format(Info.SearchUrl, options.Name));
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
                Dictionary<string, string> result = new Dictionary<string, string>();
                CQ dom = WebHelper.GetHttpResponse(Info.MainPage);
                dom = dom["select.searchSelect"].FirstOrDefault().InnerHTML;

                foreach (var item in dom["option"])
                {
                    CQ listItem = item.InnerHTML;
                    result.Add(WebUtility.HtmlDecode(item.InnerText), item.GetAttribute("value"));
                }

                return result;
            }
        }

        public void Init(Shop info)
        {
            this.Info = info;
        }
    }
}
