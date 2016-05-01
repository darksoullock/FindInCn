using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote
{
    public interface IRemoteShop
    {
        string Name { get; }

        string Url { get; }

        IDictionary<string, string> Categories { get; }

        IEnumerable<IRemoteSearchItem> Search(SearchOptions options);

        void Init(string name, string url, string searchUrl, string categoriesUrl);
    }
}
