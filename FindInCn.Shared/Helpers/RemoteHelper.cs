using FindInCn.Shared.Models;
using FindInCn.Shared.Models.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Helpers
{
    public class RemoteHelper
    {
        public static IEnumerable<IRemoteSearchItem> Search(IEnumerable<IRemoteShop> shops, SearchOptions options)
        {
            List<IRemoteSearchItem> result = new List<IRemoteSearchItem>();
            foreach(var i in shops)
            {
                result.AddRange(i.Search(options));
            }

            Random rnd = new Random();
            // TODO Performance -- write custom shuffle
            return result.OrderBy(i=>rnd.Next());
        }

        public static RemoteItemDetails GetRemoteItemDetails(int shopId, string url)
        {
            throw new NotImplementedException();
        }
    }
}
