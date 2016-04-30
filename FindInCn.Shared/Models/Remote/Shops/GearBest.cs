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

        public IEnumerable<IRemoteSearchItem> Search()
        {
            if (SearchUrl==null)
            {
                throw new ArgumentNullException(NotInitializedExceptionMessage);
            }

            throw new NotImplementedException();
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
