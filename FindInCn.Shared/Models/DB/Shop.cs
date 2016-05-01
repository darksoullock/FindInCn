using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.DB
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopId { get; set; }

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string SearchUrl { get; set; }

        public string MainPage { get; set; }
    }
}
