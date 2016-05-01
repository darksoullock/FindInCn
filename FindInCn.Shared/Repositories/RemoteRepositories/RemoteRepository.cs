using FindInCn.Shared.Models.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FindInCn.Shared.Repositories.RemoteRepositories
{
    public class RemoteRepository
    {
        public IEnumerable<IRemoteShop> GetAllShops()
        {
            var _db = new Models.DB.CnContext();
            var shops = _db.Shops.ToArray();
            List<IRemoteShop> result = new List<IRemoteShop>();
            foreach (var i in shops)
            {
                IRemoteShop shop = Activator.CreateInstance(Type.GetType(i.ClassName)) as IRemoteShop;
                shop.Init(i.Name, i.MainPage, i.SearchUrl, i.MainPage);
                result.Add(shop);
            }

            return result;
        }
    }
}
