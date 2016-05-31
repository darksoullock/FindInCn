using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Repositories.DbRepositories
{
    public class ShopRepository : DbRepository
    {
        public IQueryable<Shop> GetShops()
        {
            return _db.Shops;
        }

        public Shop GetShopById(int id)
        {
            return _db.Shops.FirstOrDefault(i => i.ShopId == id);
        }

        public Shop GetShopByName(string name)
        {
            return _db.Shops.FirstOrDefault(i => i.Name == name);
        }
    }
}
