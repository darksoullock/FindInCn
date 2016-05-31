using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Repositories.DbRepositories
{
    public class AccountRepository : DbRepository
    {
        public User GetUserById(int id)
        {
            return _db.Users.Where(i => i.Id == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.Where(i => i.Email == email).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void AddToFavorite(FavoriteItem item)
        {
            _db.Favorites.Add(item);
            _db.SaveChanges();
        }

        public IQueryable<FavoriteItem> GetFavoritesByUserIdAsQueryable(int userId)
        {
            return _db.Favorites.Where(i => i.UserId == userId);
        }

        public void RemoveFromFavorites(int id, string url)
        {
            url = WebUtility.UrlDecode(url);
            var item = _db.Favorites.FirstOrDefault(i => i.ItemUrl == url);
            if (item?.UserId==id)
            {
                _db.Favorites.Remove(item);
                _db.SaveChanges();
            }
            
        }
    }
}
