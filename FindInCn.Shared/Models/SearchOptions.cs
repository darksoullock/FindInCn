using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models
{
    public class SearchOptions
    {
        public string Name { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceTo { get; set; }
    }
}
