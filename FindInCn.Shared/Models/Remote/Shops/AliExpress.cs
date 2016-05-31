using FindInCn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Net;
using FindInCn.Shared.Models.DB;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class AliExpress : IRemoteShop
    {
        public AliExpress(Shop info)
        {
            Init(info);
        }

        public Shop Info { get; set; }

        public IEnumerable<IRemoteSearchItem> Search(SearchOptions options)
        {
            var data = WebHelper.GetHttpResponse(string.Format(Info.SearchUrl, options.Name));
            CQ dom = data.Replace("image-src", "src");
            List<IRemoteSearchItem> result = new List<IRemoteSearchItem>();

            foreach (var item in dom["li.list-item"])
            {
                CQ listItem = item.InnerHTML;
                result.Add(new GenericSearchItem()
                {
                    Name = listItem["img.picCore"].FirstOrDefault()?.GetAttribute("alt"),
                    ImageUrl = listItem["img.picCore"].FirstOrDefault()?.GetAttribute("src"),
                    Url = listItem["a.picRind"].FirstOrDefault()?.GetAttribute("href"),
                    PriceString = WebUtility.HtmlDecode(listItem["span.value"].FirstOrDefault(i => i.HasAttribute("itemprop") && i.GetAttribute("itemprop") == "price")?.InnerText),
                    ShopId = Info.ShopId
                });
            }

            //Console.WriteLine($"store -- {listItem["a.store"].FirstOrDefault()?.GetAttribute("title")}");
            //Console.WriteLine($"store address -- {listItem["a.store"].FirstOrDefault()?.GetAttribute("href")}");
            //Console.WriteLine($"seller score -- {listItem["span.score-icon-new"].FirstOrDefault()?.GetAttribute("sellerPositiveFeedbackPercentage")}");
            //Console.WriteLine($"seller feedback -- {listItem["a.score-dot"].FirstOrDefault()?.GetAttribute("href")}");
            return result;
        }

        public IDictionary<string, string> Categories
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                CQ dom = WebHelper.GetHttpResponse(Info.MainPage);

                foreach (var item in dom["dl.sub-cate-items dt a"])
                {
                    CQ listItem = item.InnerHTML;
                    var key = WebUtility.HtmlDecode(item.InnerText);
                    result[key] = item.GetAttribute("href");
                }

                return result;
            }
        }

        public void Init(Shop shop)
        {
            this.Info = shop;
        }

        public RemoteItemDetails GetItem(string url)
        {
            var item = new RemoteItemDetails();
            item.ShopId = this.Info.ShopId;
            item.ItemUrl = url;

            if (url.StartsWith("//"))
            {
                url = "http:" + url;
            }

            //url = url.Split('?').First();

            CQ dom = WebHelper.GetHttpResponse(url);
            item.ImageUrl = dom["meta"]?.FirstOrDefault(i => i.GetAttribute("property") == "og:image")?.GetAttribute("content");
                        
            item.Title = WebUtility.HtmlDecode(dom["title"].Html());

            dom = dom[".product-property-list"];
            item.Properties = new List<ProductPropertyItem>(16);

            foreach (var i in dom["li.property-item"])
            {
                CQ listItem = i.InnerHTML;
                var p = new ProductPropertyItem();
                p.Name = WebUtility.HtmlDecode(listItem[".propery-title"].Html());
                p.Value = WebUtility.HtmlDecode(listItem[".propery-des"].Html());
            }
            
            return item;
        }
    }
}