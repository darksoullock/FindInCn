using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote
{
    public interface IRemoteShop
    {
        Shop Info { get; }

        IDictionary<string, string> Categories { get; }

        IEnumerable<IRemoteSearchItem> Search(SearchOptions options);

        void Init(Shop info);

        RemoteItemDetails GetItem(string url);
    }
}
