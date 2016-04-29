using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models
{
    public class CnContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
    }
}
