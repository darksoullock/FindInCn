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
            dom = dom["div.catePro_ListBox"].FirstOrDefault()?.InnerHTML;
            if (dom == null)
            {
                return result;
            }

            foreach (var item in dom["li"])
            {
                CQ listItem = item.InnerHTML;
                var link = listItem["p.all_proNam a"].FirstOrDefault();
                if (link == null)
                {
                    continue;
                }

                result.Add(new GenericSearchItem()
                {
                    Name = link.GetAttribute("title"),
                    Url = link.GetAttribute("href"),
                    ImageUrl = listItem["img"].FirstOrDefault().GetAttribute("data-original"),
                    PriceString = listItem["span.my_shop_price"].FirstOrDefault().InnerText + "USD",
                    ShopId = Info.ShopId
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

        public RemoteItemDetails GetItem(string url)
        {
            CQ dom = WebHelper.GetHttpResponse(url);
            var item = new RemoteItemDetails();
            item.ShopId = this.Info.ShopId;
            item.ItemUrl = url;
            item.ImageUrl = dom["meta"]?.FirstOrDefault(i => i.GetAttribute("property") == "og:image")?.GetAttribute("content");
            item.Title = WebUtility.HtmlDecode(dom["title"].Html().Trim());
            var properties = new List<ProductPropertyItem>(16);

            foreach (var i in dom[".product_pz_style2 table tr td"])
            {
                string listItem = i.InnerHTML;

                listItem.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(j => j.Trim().Split(':')).ToList()
                    .ForEach(j => properties.Add(new ProductPropertyItem() { Name = j.First(), Value = j.Last() }));
            }

            foreach (var i in dom["li.property-item"])
            {
                CQ listItem = i.InnerHTML;
                var p = new ProductPropertyItem();
                p.Name = WebUtility.HtmlDecode(listItem[".propery-title"].Html());
                p.Value = WebUtility.HtmlDecode(listItem[".propery-des"].Html());
            }

            item.Properties = properties;
            return item;
        }
    }
}
