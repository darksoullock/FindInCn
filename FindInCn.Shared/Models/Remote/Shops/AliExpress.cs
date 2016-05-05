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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Logo
        {
            get { return "https://lh3.googleusercontent.com/nHVoq8tujVi-kNdEWWUivSxPCc9XEtT7Nk10oGfkyg6KdU0dPkOulKbVOLTj_4bDEu8=w300"; }
        }

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

                Dictionary<string, string> result = new Dictionary<string, string>();
                CQ dom = WebHelper.GetHttpResponse(CategoriesUrl);

                foreach (var item in dom["dl.sub-cate-items dt a"])
                {
                    CQ listItem = item.InnerHTML;
                    var key = WebUtility.HtmlDecode(item.InnerText);
                    result[key] = item.GetAttribute("href");
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