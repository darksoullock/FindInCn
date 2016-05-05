using FindInCn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Net;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class AliExpress : IRemoteShop
    {
        public AliExpress()
        {

        }

        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<IRemoteSearchItem> Search(SearchOptions options)
        {
            if (SearchUrl == null)
            {
                throw new ArgumentNullException(NotInitializedExceptionMessage);
            }

            var data = WebHelper.GetHttpResponse(string.Format(SearchUrl, options.Name));
            CQ dom = data.Replace("image-src", "src");
            List<IRemoteSearchItem> result = new List<IRemoteSearchItem>();

            foreach (var item in dom["li.list-item"])
            {
                CQ listItem = item.InnerHTML;
                result.Add(new GenericSearchItem<AliExpress>()
                {
                    Name = listItem["img.picCore"].FirstOrDefault()?.GetAttribute("alt"),
                    ImageUrl = listItem["img.picCore"].FirstOrDefault()?.GetAttribute("src"),
                    Url = listItem["a.picRind"].FirstOrDefault()?.GetAttribute("href"),
                    PriceString = WebUtility.HtmlDecode(listItem["span.value"].FirstOrDefault(i => i.HasAttribute("itemprop") && i.GetAttribute("itemprop") == "price")?.InnerText)
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