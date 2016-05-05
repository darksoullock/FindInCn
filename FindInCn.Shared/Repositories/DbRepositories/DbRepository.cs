using FindInCn.Shared.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Repositories.DbRepositories
{
    public class DbRepository
    {
        protected CnContext _db = new CnContext();
    }
}
