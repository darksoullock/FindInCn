using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote
{
    public class RemoteItemDetails
    {
        public int ShopId { get; set; }

        public string Title { get; set; }

        public string ItemUrl { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ProductPropertyItem> Properties { get; set; }
    }
}
