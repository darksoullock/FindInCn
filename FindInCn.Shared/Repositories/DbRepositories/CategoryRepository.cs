using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Repositories.DbRepositories
{
    public class CategoryRepository : DbRepository
    {
        public IQueryable<Category> GetCategories()
        {
            return _db.Categories;
        }
    }
}
