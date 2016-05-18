using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
