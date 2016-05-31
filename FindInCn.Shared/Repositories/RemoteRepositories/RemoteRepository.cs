using FindInCn.Shared.Models.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FindInCn.Shared.Repositories.DbRepositories;

namespace FindInCn.Shared.Repositories.RemoteRepositories
{
    public class RemoteRepository
    {
        public IEnumerable<IRemoteShop> GetRemoteShops()
        {
            var shopRepository = new ShopRepository();
            var shops = shopRepository.GetShops().ToArray();
            List<IRemoteShop> result = new List<IRemoteShop>();
            foreach (var i in shops)
            {
                IRemoteShop shop = Activator.CreateInstance(Type.GetType(i.ClassName), i) as IRemoteShop;
                result.Add(shop);
            }

            return result;
        }

        public IRemoteShop GetRemoteShop(int id)
        {
            var shopRepository = new ShopRepository();
            var item = shopRepository.GetShopById(id);
            IRemoteShop shop = Activator.CreateInstance(Type.GetType(item.ClassName), item) as IRemoteShop;
            return shop;
        }

        public IRemoteShop GetRemoteShop(string name)
        {
            var shopRepository = new ShopRepository();
            var item = shopRepository.GetShopByName(name);
            IRemoteShop shop = Activator.CreateInstance(Type.GetType(item.ClassName), item) as IRemoteShop;
            return shop;
        }
    }
}
