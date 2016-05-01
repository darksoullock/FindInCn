using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class GenericSearchItem<T> : IRemoteSearchItem where T : IRemoteShop
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public string Currency { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public T Shop { get; set; }
    }
}