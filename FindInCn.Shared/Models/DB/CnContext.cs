using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.DB
{
    public class CnContext : DbContext
    {
        public CnContext() : base("name=FindInCnConnectionString")
        {

        }

        public DbSet<Shop> Shops { get; set; }
    }
}
